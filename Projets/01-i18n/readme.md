<p align="Center"><img src="../../includes/logo.png" alt="drawing" width="100"/></p>
<h4 align="Center">1SS - Sujet Spéciaux</h4>

# 🏋🏻‍♂️ Projet 1 - Internationnalisation (i18n)

## 1. Créer les fichiers de langue

1. Créer un fichier de langue primaire nommé `i18n.resx` pour l'anglais et un fichier pour chacune des langues secondaires au format `i18n.[CODE DE LANGUE].resx`. Dans notre cas le fichier se nommera i18n.fr-CA.resx.
   - Project / Properties / Resources / General / Create or open assembly resources.

3. Maximisez la configuration des fichiers de langue pour une utilisation plus efficace :
   1. Supprimez le fichier de code `[CODE DE LANGUE].designer.cs de tous les fichier de langue **secondaires**.
   2. Supprimez la valeur de la clé `Custom Tool` dans les propriétés de tous les fichiers de langue **secondaires**.
   3. Placer tous les fichier de classe à `Public` au lieu d'`Internal`.
4. Ouvrez `i18n.resx` et entrez une clé et les valeurs pour toutes les parties de texte **statiques** (sans variables) à internationaliser.
   - `Neutral Value` représente la langue de base, ici l'anglais.
   - Une colonne par langue supplémentaire sera affichée afin d'y entrer la traduction dans cette langue.
   - Exemple :

     | Name        | Neutral Value     | fr-CA (French (Canada))            |
     | ----------- | ----------------- | ---------------------------------- |
     | APP_TITLE   | NHL Teams Manager | Gestionnaire des équipes de la NHL |
     | FULL_NAME   | Full name         | Nom complet                        |
     | WORD_NUMBER | Number            | Numéro                             |

     > ⚠️ Ne pas créer toutes les traductions immédiatement car il y aura des nuances pour les textes contenant des variables.

## 2. Traduire les éléments graphiques statiques

1. Importer les ressources de langue dans chacune des `Views` en ajoutant le namespace **properties** que nous nommerons `p`.
   - Ajouter la valeur `"clr-namespace:[PROJECT_NAME].Properties"` à la clé `xmlns:p` dans la section `Window`.
2. Remplacer les valeurs statique de la clé d'Affichage (`Title`, `Content`, `Header`, etc.) des éléments graphique par la propriété dynamique `"{x:Static p:i18n.[KEY]}"`

   > ⚠️ S'il existe des spécification de langue statiques, il faut les retirés. Par exemple :

   ```xml
       <Run Language="fr-ca"
        Text="Équipes" />
   ```

## 3. Traduire dans le **code**
Simplement accéder à la ressource en utilisant le namespace `i18n`.
    ```csharp
    i18n.DELETE_CONFIRMATION
    ```

## 4. Utilisation de textes dynamiques (avec variables)

1. Utiliser le format litéral pour les chaînes de caractères en la débutant par un `$` et en insérant les variables dans des `{}`. Exemple :
   ```csharp
   StatusMessage = $"{trimmedName} a été ajouté à {SelectedTeam.Name}.";
   ```
2. Ceci permettra de faire la corrélation des variables dans vos fichiers de langue en les numérottant. Exemple :
   | Name | Neutral Value | fr-CA (French (Canada)) |
   | ----------- | ----------------- | ---------------------------------- |
   | MESSAGE_DELETE_PLAYER | Delete player {SelectedPlayer.FullName}? | Supprimer le joueur{SelectedPlayer.FullName}? |
   | STATUS_TEAM_ADDED | {1} has a new player {0}! | {0} a été ajouté à {1}. |

3. Utiliser la fonction `Format` de la classe `string` afin de remplacer les `{0}` des fichier de ressouces de lanque par la variable appropriée. Exemple : 
    ```csharp
    MessageBoxResult result = MessageBox.Show(
    string.Format(
        i18n.DELETE_CONFIRMATION,
        SelectedPlayer.FullName),
    i18n.DELETE_CONFIRMATION_TITLE,
    MessageBoxButton.YesNo,
    MessageBoxImage.Question);
    ```
## 5. Les ressources images

[À venir]
<hr><p align="Center"><img src="../../includes/end.png" alt="drawing" width="150"/></p>