<p align="Center"><img src="../../includes/logo.png" alt="drawing" width="100"/></p>

<h4 align="Center">1SS - Sujets spéciaux</h4>

# 🏋🏻‍♂️ Projet 2 - CherryPicker (v.0.8) - 25%

Infologique Innovations est une entreprise œuvrant dans la recherche et le développement qui a présentement un souci avec sa gestion documentaire. Elle possède [plusieurs milliers de fichiers de rapports importants](./includes/Archives.zip) et en extraire des données est devenu une tâche bien ardue et coûteuse.

Elle vous a donc mandaté afin de développer un logiciel utilitaire permettant de faire des recherches personnalisées dans l'ensemble des fichiers contenus dans les sous-répertoires d'un répertoire de base. Votre utilitaire fera économiser beaucoup d'argent à l'entreprise.

Cédrik Dubogue, employé d'Infologique Innovations, a [débuté le projet](<./includes/CherryPicker%20(Base%20Project).zip>) en créant l'interface utilisateur et vous demande de compléter l'utilitaire en y ajoutant la logique métier.

Voici le cahier des charges soumis par Infologique Innovations.

# Cahier des charges — Projet logiciel

**Nom du logiciel :** CherryPicker  
**Type de projet :** Utilitaire de recherche spécialisée

## 1. Présentation du projet

**Description :**  
CherryPicker est un utilitaire permettant de rechercher du texte à l'intérieur des fichiers d'un répertoire et de ses sous-répertoires en permettant l'utilisation de filtres sous forme d'expressions régulières.

**Problématique :**  
La recherche manuelle d'informations dans un grand nombre de fichiers est longue et fastidieuse et coûte très cher à l'entreprise.

**Objectif :**  
Développer un outil simple et rapide permettant d'automatiser cette recherche.

## 2. Exigences fonctionnelles

| ID | Fonctionnalité | Priorité |
|---|---|---|
| F01 | Sélectionner un répertoire de base | Obligatoire |
| F02 | Parcourir le répertoire et ses sous-répertoires | Obligatoire |
| F03 | Rechercher une expression régulière dans les fichiers texte | Obligatoire |
| F04 | Afficher les fichiers correspondants | Obligatoire |
| F05 | Être en mesure d'annuler l'opération | Obligatoire |
| F06 | Afficher l'emplacement (numéro de ligne et numéro de colonne) du résultat dans le fichier | Souhaitable |
| F07 | Afficher un aperçu du résultat dans le fichier | Souhaitable |
| F08 | Afficher une progression réelle | Souhaitable |

## 4. Exigences non fonctionnelles

- **Performance :** l'interface doit demeurer réactive pendant une recherche.
- **Robustesse :** les fichiers inaccessibles ne doivent pas provoquer l'arrêt de l'application.
- **Fiabilité :** une recherche doit retourner tous les fichiers accessibles correspondant aux critères.

## 5. Interface utilisateur

### Ouverture de l'application
![Ouverture](./includes/ui01.png)

### Après le résultat de la recherche
![Résultat](./includes/UI02.png)

## 6. Limites et exclusions

- Aucune recherche dans des fichiers PDF ou Word.
- Aucune modification des fichiers analysés.
- Aucune recherche sur des ordinateurs distants.
- Aucun système d'authentification.

## 7. Livrables
- Code source complet.

## 8. Critères d'acceptation

- La recherche fonctionne sur une arborescence d'au moins 10 niveaux.
- Une recherche sans résultat affiche un message approprié.
- Les résultats indiquent le chemin complet des fichiers.
- Un fichier inaccessible ne fait pas planter l'application.
- L'interface demeure utilisable pendant la recherche.
- En tout temps, il doit être possible d'annuler une recherche.

---

## Annexe — Considérations techniques ?

| Cahier des charges | Considérations techniques |
|---|---|
| Le logiciel doit parcourir tous les sous-répertoires. | Utiliser une fonction récursive. |
| L'interface doit demeurer réactive. | Utiliser `async/await`. |
| Les résultats doivent apparaître dans une liste. | Utiliser un `DataGrid` WPF. |
| Le logiciel doit gérer les erreurs d'accès. | Utiliser `try/catch`. |

# Suggestion de structure de travail pour débuter

## SearchResultItem
Classe `Models` servant simplement à enregistrer un résultat unique de recherche avec, au minimum, les attributs `filePath` (fichier source) et `match` (ce qui a été trouvé).

## FileSearchService
1. Faire un `singleton` de cette classe.
2. Ajouter `using System.Text.RegularExpressions;`
3. Programmer, par étapes, une fonction de recherche dans un fichier précis :
   1. Débutez par le noyau de la fonction, la recherche d'expressions régulières :
      ```csharp
      public void SearchFile(string filePath, Regex regex) {
          // Lire le fichier dans une variable
          // Appliquer la Regex à cette variable
          // Afficher les résultats à la console pour commencer
      }
      ```
      - Testez avec [ce fichier](./includes/test.txt) au besoin.
   2. Transmettre les résultats de recherche dans un rapport `SearchResultItem` à l'aide d'un `Progress`.
      ```csharp
      public void SearchFile(string filePath, Regex regex, IProgress<SearchResultItem> results) {
          // Déclencher la fonction `Report` du Progress `results` pour chaque résultat.
      }
      ```
   3. Programmer une fonction de recherche de répertoire qui sera utilisée de façon récursive.
      ```csharp
      public void SearchDirectoryRecursive(string directoryPath, Regex regex, IProgress<SearchResultItem> results) {
          // Pour chacun des fichiers dans ce répertoire
          // Lancer la fonction SearchFile

          // Pour chacun des répertoires dans ce répertoire
          // Relancer la fonction SearchDirectoryRecursive
      }
      ```
   4. Programmer une fonction racine qui s'occupera de lancer la fonction récursive.
      ```csharp
      public void SearchDirectory(string directoryPath, Regex regex, IProgress<SearchResultItem> results) {
          // Initialiser la recherche ici

          // Lancer la fonction SearchDirectoryRecursive
      }
      ```
   5. Ajouter la possibilité de passer un `IProgress` retournant un rapport sur l'état de la recherche (`SearchProgressReport`).
   6. Rendre ces fonctions asynchrones en modifiant leur signature et en ajoutant des `await` aux bons endroits.

# Critères de correction (version finale à venir)

| # | Critère | Points |
| --- | --------------- | ----- |
| 01 | La boîte de dialogue s'ouvre par défaut dans le répertoire où se trouve l'exécutable | 1 |
| 02 | Possibilité de sélectionner un répertoire | 1 |
| 03 | Il est impossible de lancer une recherche avec une Regex invalide | 2 |
| 04 | L'application présente l'ensemble des données, et ce, en temps réel | 2 |
| 05 | La recherche se fait totalement de manière asynchrone | 5 |
| 06 | Il est possible d'annuler une tâche | 2 |
| 07 | Utilisation correcte des classes de progression | 2 |
| 08 | Utilisabilité générale et robustesse de l'application | 2 |
| 09 | Une logique UX est appliquée pour l'activation des boutons | 2 |
| 10 | Flexibilité et maintenance de l'architecture appliquée | 2 |
| P | Élément de code non conforme aux exigences vues dans le programme (par erreur) | -0,5 |

### Bonus

| # | Élément | Points |
| --- | --------------- | ----- |
| 01 | Afficher l'emplacement (numéro de ligne et numéro de colonne) du résultat dans le fichier | 1 |
| 02 | Afficher un aperçu du résultat dans le fichier | 1 |
| 03 | Afficher une progression réelle | 1 |

<hr><p align="Center"><img src="../../includes/end.png" alt="drawing" width="150"/></p>
