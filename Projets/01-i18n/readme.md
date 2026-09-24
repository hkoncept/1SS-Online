<p align="Center"><img src="../../includes/logo.png" alt="drawing" width="100"/></p>

<h4 align="Center">1SS - Sujets spéciaux</h4>

# 🏋🏻‍♂️ Projet 1 - Internationalisation (i18n) - 15%

## 1. Créer les fichiers de langue

1. Créer un fichier de langue primaire nommé `i18n.resx` pour l'anglais et un fichier pour chacune des langues secondaires au format `i18n.[CODE DE LANGUE].resx`. Dans notre cas, le fichier se nommera `i18n.fr-CA.resx`.

   - Project / Properties / Resources / General / Create or open assembly resources.

3. Maximisez la configuration des fichiers de langue pour une utilisation plus efficace :

   1. Supprimez le fichier de code `[CODE DE LANGUE].designer.cs` de tous les fichiers de langue **secondaires**.

   2. Supprimez la valeur de la clé `Custom Tool` dans les propriétés de tous les fichiers de langue **secondaires**.

   3. Placez tous les fichiers de classe à `Public` au lieu d'`Internal`.

4. Ouvrez `i18n.resx` et entrez une clé et les valeurs pour toutes les parties de texte **statiques** (sans variables) à internationaliser.

   - `Neutral Value` représente la langue de base, ici l'anglais.

   - Une colonne par langue supplémentaire sera affichée afin d'y entrer la traduction dans cette langue.

   - Voici la [liste des clés à utiliser](./keylist.md).

   - Exemple :

     | Name        | Neutral Value     | fr-CA (French (Canada))             |
     | ----------- | ----------------- | ----------------------------------- |
     | APP_TITLE   | NHL Teams Manager | Gestionnaire des équipes de la NHL |
     | FULL_NAME   | Full name         | Nom complet                         |
     | WORD_NUMBER | Number            | Numéro                              |

     > ⚠️ Ne pas créer toutes les traductions immédiatement, car il y aura des nuances pour les textes contenant des variables.

## 2. Traduire les éléments graphiques statiques

1. Importer les ressources de langue dans chacune des `Views` en ajoutant le namespace **properties** que nous nommerons `p`.

   - Ajouter la valeur `"clr-namespace:[PROJECT_NAME].Properties"` à la clé `xmlns:p` dans la section `Window`.

2. Remplacer les valeurs statiques de la clé d'affichage (`Title`, `Content`, `Header`, etc.) des éléments graphiques par la propriété dynamique `"{x:Static p:i18n.[KEY]}"`.

   > ⚠️ S'il existe des spécifications de langue statiques, il faut les retirer. Par exemple :

   ```xml
   <Run Language="fr-ca"
        Text="Équipes" />
   ```

# 3. Traduire dans le code

Accéder simplement à la ressource en utilisant le namespace `i18n`. Exemple : `i18n.DELETE_CONFIRMATION`

# 4. Utilisation de textes dynamiques (avec variables)

Utiliser le format littéral pour les chaînes de caractères en les faisant débuter par un `$` et en insérant les variables dans des `{}`. Exemple :

```csharp
StatusMessage = $"{trimmedName} a été ajouté à {SelectedTeam.Name}.";
```

Ceci permettra de faire la corrélation des variables dans vos fichiers de langue en les numérotant. Exemple :

| Name	| Neutral Value	| fr-CA (French (Canada))
| ---------- | ---------- | ----------------- |
| MESSAGE_DELETE_PLAYER | Delete player {0}? |	Supprimer le joueur {0}? |
| STATUS_TEAM_ADDED |	{1} has a new player {0}! |	{0} a été ajouté à {1}. |

Utiliser la fonction Format de la classe string afin de remplacer les {0} des fichiers de ressources de langue par la variable appropriée. Exemple :

```csharp
MessageBoxResult result = MessageBox.Show(
    string.Format(
        i18n.DELETE_CONFIRMATION,
        SelectedPlayer.FullName),
    i18n.DELETE_CONFIRMATION_TITLE,
    MessageBoxButton.YesNo,
    MessageBoxImage.Question);
```

# 5. Les ressources images

Trouvez le moyen d'avoir un [logo NHL en anglais et LNH en français](./includes/logos.zip).

> 💡Indice 1: Est-ce que les valeurs des fichiers de langue peuvent contenir un chemin d'accès à un fichier image ?

> 💡Indice 2: Il faudra passer le résultat de l'indice précédent dans la valeur de la clé __Source__ de __Binding__.  Exemple :

```csharp
{Binding Source={[INDICE 1 ICI]}}
```

# Critères de correction
| #	| Critère	| Points |
| --- | --------------- | ----- |
| 1 | Justesse du langage utilisé (français & anglais)  |  3 |
| 2 | Fonctionnement de la traduction textuelle UI  |  5 |
| 3 | Fonctionnement de la traduction textuelle provenant du code  |  5 |
| 4 | Changement de logo selon la langue  |  2 |
| 5 | Implication de l'étudiant dans le Git du projet  |  - |
<hr><p align="Center"><img src="../../includes/end.png" alt="drawing" width="150"/></p> ```