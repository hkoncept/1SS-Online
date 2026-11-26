<p align="Center"><img src="../../../includes/logo.png" alt="drawing" width="100"/></p>

<h4 align="Center">1SS - Sujets spéciaux</h4>

# 🏋🏻‍♂️ Projet 4.1 - MVVM Toolkit

#### Binding d'événement utilisant les annotations de propriétés

## Mission et objectifs

Refactoriser l’application avec `CommunityToolkit.Mvvm` en conservant ses deux fenêtres. Vous devez pouvoir expliquer les propriétés générées, préserver les effets des setters, gérer l’autorisation des commandes et vérifier l’actualisation de l’affichage.

`MainViewModel` gère les sélections et l’ouverture du dialogue. `PlayerViewModel` gère les saisies et les opérations. Les champs saisis ne doivent modifier la base et le joueur partagé qu’après Enregistrer. Le projet contient déjà un service SQLite, les styles et les traductions nécessaires.

## Exigences observables

- Les propriétés de présentation des deux ViewModels utilisent le Toolkit.
- Les six commandes sont générées depuis des méthodes annotées.
- L’activation des boutons réagit immédiatement aux changements pertinents.
- Les commandes Enregistrer et Annuler reçoivent la fenêtre par leur paramètre.
- Ajouter, modifier, supprimer, annuler, changer de langue et réinitialiser fonctionnent.
- Le compteur se met à jour après un ajout ou une suppression sans changer d’équipe.
- Les noms des Bindings publics restent ceux du projet de départ.

## Étape 1 — Observer le départ : 10 min

1. Extraire `NHLTeams_Depart_v1.1.zip` dans un dossier distinct et ouvrir `NHLTeams.slnx`.
2. Restaurer les packages et lancer l’application.
3. Sélectionner un joueur et ouvrir le formulaire de modification. Changer son nom, puis annuler. Observer la liste.
4. Ajouter un joueur avec un numéro disponible. Observer séparément la liste et le compteur.
5. Repérer le code qui crée les commandes et les deux endroits où se trouve une propriété `FullName`.

**À noter :** lequel de ces deux `FullName` représente le brouillon, et lequel représente l’objet partagé?

## Étape 2 — Migrer les propriétés du formulaire : 20 min

Installer `CommunityToolkit.Mvvm` version **8.4.0** dans le projet :

```powershell
dotnet add NHLTeams/NHLTeams.csproj package CommunityToolkit.Mvvm --version 8.4.0
```

1. Remplacer l’implémentation manuelle de `BaseViewModel` par l’héritage d’`ObservableObject`.
2. Déclarer `PlayerViewModel` comme classe partielle.
3. Migrer `FullName`, `NumberText` et `ErrorMessage` avec `[ObservableProperty]`.
4. Conserver l’effacement du message d’erreur lors d’une nouvelle saisie à l’aide des hooks.
5. Utiliser les propriétés publiques pour initialiser le nom et le numéro d’un joueur existant.

Exemple de technique, sur un champ distinct à ne pas ajouter au projet :

```csharp
[ObservableProperty]
private string message = string.Empty;
// La propriété publique générée se nomme Message.
```

Ne conserver qu’une seule définition de chaque propriété. `IsSaved` et `ResultMessage` peuvent rester des propriétés ordinaires, car le ViewModel principal les lit après le dialogue.

**Contrôle :** le formulaire se remplit encore; une erreur disparaît à la modification du nom ou du numéro.

## Étape 3 — Migrer les commandes du formulaire : 25 min

1. Ajouter l’import du namespace `CommunityToolkit.Mvvm.Input`.
2. Annoter les méthodes Enregistrer et Annuler avec `[RelayCommand]`.
3. Remplacer le paramètre `object?` par un paramètre `Window?` et enlever les conversions explicites vers `Window`.
4. Relier la commande d’enregistrement à `CanSave`.
5. Mettre à jour cette condition : fenêtre présente; en ajout/modification, nom non vide et entier entre 1 et 99; en suppression, confirmation permise.
6. Supprimer les anciennes propriétés de commandes et leurs initialisations manuelles.
7. Déclarer les dépendances pour réévaluer l’enregistrement lorsque le nom ou le numéro change.
8. Conserver les deux Bindings `CommandParameter` qui transmettent la fenêtre dans `PlayerView.xaml`.

Conserver les opérations SQLite, les messages, le résultat de l’opération et la fermeture après succès. Une erreur doit garder le formulaire ouvert; Annuler doit le fermer sans sauvegarde.

**Contrôle :** dans Ajouter, saisir un nom et `42`, puis remplacer `42` par `abc`, `0`, `100` et enfin un numéro valide. Vérifier le bouton à chaque frappe, sans cliquer ailleurs. Pour enregistrer réellement, choisir un numéro libre dans l’équipe.

## Étape 4 — Migrer la fenêtre principale : 25 min

1. Rendre `MainViewModel` partielle et migrer ses propriétés de sélection, sa collection de joueurs, son statut et sa langue.
2. Attention au champ `currentCulture` : le Binding existant doit continuer d’accéder à `SelectedCulture`.
3. Déplacer les effets du changement d’équipe et du changement de langue vers les hooks appropriés.
4. Créer des méthodes pour Ajouter, Modifier et Supprimer, qui appellent `OpenPlayerView` avec l’opération appropriée.
5. Générer les quatre commandes, y compris Réinitialiser, et retirer leur construction manuelle.
6. Déclarer les dépendances des commandes sur les sélections.
7. Retirer l’import de la commande maison des deux ViewModels. Rechercher ses autres utilisations avant de supprimer son fichier.

| Commande principale | Autorisation attendue |
|---|---|
| Ajouter | Une équipe est sélectionnée |
| Modifier | Une équipe et un joueur sont sélectionnés |
| Supprimer | Une équipe et un joueur sont sélectionnés |
| Réinitialiser | Toujours disponible; confirmation dans la méthode |

Le formulaire possède sa propre validation; la fenêtre principale ne doit pas devenir propriétaire de ses champs.

**Contrôle :** changer d’équipe efface la sélection du joueur. Les icônes crayon et corbeille se désactivent; le bouton Ajouter demeure disponible si une équipe est sélectionnée.

## Étape 5 — Rendre le compteur fiable : 25 min

Travail autonome. Le texte `PlayerCountText` dépend de l’équipe choisie et du nombre de joueurs. Dans cette version, `DataService` conserve une même collection par équipe.

Exigences :

1. Le compteur doit être recalculé après un changement d’équipe ou un remplacement de `Players`.
2. Il doit aussi changer lors d’un ajout ou d’une suppression dans la collection actuelle.
3. Changer plusieurs fois d’équipe ne doit pas accumuler des abonnements au même événement.
4. Une suppression ne doit pas laisser le joueur retiré sélectionné.

Vous pouvez utiliser les hooks du Toolkit, `[NotifyPropertyChangedFor]`, `CollectionChanged` et une notification explicite lorsque nécessaire. Ne pas recharger toutes les données SQL simplement pour rafraîchir le texte.

**Contrôle :** ajouter un joueur, le supprimer, changer trois fois d’équipe et refaire l’essai. Comparer le nombre de lignes au compteur.

## Étape 6 — Vérifier et expliquer : 15 min

Compléter le tableau ci-dessous. Les données de démonstration peuvent être réinitialisées par le menu après confirmation.

| Test | Résultat attendu | Résultat observé |
|---|---|---|
| Démarrer | Équipes et joueurs affichés; compteur cohérent | |
| Aucune sélection de joueur | Modifier et Supprimer désactivés | |
| Ouvrir Ajouter | Formulaire vide; Enregistrer désactivé | |
| Nom vide ou composé d’espaces | Enregistrer désactivé | |
| Numéro vide, `abc`, `0` ou `100` | Enregistrer désactivé | |
| Nom valide et entier 1–99 | Enregistrer activé si fenêtre transmise | |
| Enregistrer avec un numéro libre | Dialogue fermé; nouvelle ligne et compteur augmenté | |
| Numéro déjà utilisé | Dialogue ouvert; erreur; aucune nouvelle ligne | |
| Modifier la saisie après l’erreur | Message d’erreur effacé | |
| Modifier et enregistrer | Cellules actualisées; compteur inchangé | |
| Modifier puis Annuler | Données d’origine conservées | |
| Fermer avec la croix ou Échap | Aucune sauvegarde du brouillon | |
| Annuler une suppression | Joueur conservé | |
| Confirmer une suppression | Ligne retirée; compteur diminué; sélection retirée | |
| Changer d’équipe plusieurs fois | Liste, compteur et commandes cohérents | |
| Passer du français à l’anglais et revenir | Interface recréée avec données et sélection conservées | |
| Refuser Réinitialiser | Données conservées | |
| Accepter Réinitialiser | Données du script restaurées et sélections cohérentes | |
| Redémarrer après un enregistrement | Modification persistée dans SQLite | |

Répondre en quelques phrases :

1. Qui implémente maintenant `INotifyPropertyChanged`? Pourquoi les propriétés automatiques ordinaires ne suffisent-elles pas?
2. Quelle différence entre `NotifyPropertyChangedFor` et `NotifyCanExecuteChangedFor`?
3. Pourquoi faut-il écouter la collection pour le compteur?
4. Comment la fenêtre arrive-t-elle dans `Save(Window? window)`?
5. Pourquoi Annuler ne modifie-t-il pas le joueur partagé?

## Livrables et critères de réussite

Remettre la solution source sans `bin`, `obj` ni `.vs`, le tableau de vérification et les réponses. Aucune pondération officielle n’est attribuée à cette activité formative.

- **Notifications :** pas de propriétés dupliquées; noms des Bindings conservés; hooks corrects.
- **Commandes :** six commandes générées, autorisations réévaluées sans interaction supplémentaire.
- **Comportement :** dialogue, brouillon, validation, erreurs et persistance cohérents.
- **Collections :** compteur à jour et anciens abonnements retirés.
- **Compréhension :** distinction claire entre changement de propriété, composition de collection et autorisation de commande.

## Indices gradués — après une tentative

**Indice 1 :** une méthode `Save` annotée donne `SaveCommand`. Un champ `selectedCulture` annoté donne `SelectedCulture`. Les noms générés permettent de conserver le XAML.

**Indice 2 :** pour le compteur, distinguer « nouvelle collection » et « même collection, un élément de plus ». Pour les abonnements, les hooks qui reçoivent l’ancienne et la nouvelle valeur permettent d’enlever le gestionnaire avant de le réinstaller.

## Consolidation et défi facultatif

Consolidation : stabiliser d’abord les propriétés et les deux commandes du formulaire, puis traiter Ajouter avant les autres commandes principales. Le compteur peut être terminé avec les indices.

Défi hors des 120 minutes : moderniser `Player` et `Team` avec le Toolkit, puis simplifier `DataService.Teams` avec `SetProperty` en conservant son setter privé. Vérifier que la modification du joueur partagé continue de mettre à jour les cellules sans remplacer la collection.


<hr><p align="Center"><img src="../../../includes/end.png" alt="drawing" width="150"/></p>