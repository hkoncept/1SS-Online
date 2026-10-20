<p align="Center"><img src="../../includes/logo.png" alt="drawing" width="100"/></p>

<h4 align="Center">1SS - Sujets spéciaux</h4>

# 🏋🏻‍♂️ Projet 3 - NHLStats - 15%

#### Lecture en temps réel des statistiques des joueurs.

## 1. Lecture des données d'équipes

Un client vous demande de programmer une application permettant de consulter la composition des équipes d'aujourd'hui mais également du passé. Comme vous avez déjà programmé une application NHLTeams, vous avez pris la décision de partir de ce projet et de l'adapter au besoin du nouveau client.

Pour ce projet, aucune persistance de données métier n'est authorisé. À l'ouverture de l'application et lors d'un changement d'année, elle ira lire les données des équipes ainsi que des joueurs directement de l'API de la NHL.

## 2. Lecture des statistiques d'un joueur.

Lors de la sélection d'un joueur, ses statistiques actuelles seront téléchargée de l'API de la NHL (à chaque sélection de joueur). Cela veut dire que si on sélectionne un joueur qui a 5 buts et qu'on le resélectionne plus tard et qu'il a compté un autre but, l'application se mettra à jour avec ses 6 buts.

Voici le visuel demandé par le client :
![UI](../03-NHLStats/includes/nhlstats.png)

## 3. Refactoriser un projet existant.
Comme vous avez déjà programmé une application similaire `NHLTeams`, vous devez repartir de la version actuelle du repo d'équipe orginal et effectuer le dépôt dans une nouvelle branche nommée `NHLStats`.

## 4. Information complémentaire.


<!-- # Critères de correction

| #   | Critère                                                                                   | Points |
| --- | ----------------------------------------------------------------------------------------- | ------ |
| 01  | Toutes les années disponibles sont sélectionnables                                        | 1      |
| 02  | Un changement d'année, met l'information des                                              | 1      |
| 03  | Il est impossible de lancer une recherche avec une Regex invalide                         | 2      |
| 04  | Affichage de l'avancement de la recherche (Nombre de fichiers lus et nombre de résultats) | 2      |
| 05  | Affichage correct de la liste des résultats (fichier et élément trouvé)                   | 1      |
| 06  | Extraction et affichage du mot complet                                                    | 1      |
| 07  | La recherche se fait totalement de manière asynchrone                                     | 3      |
| 08  | La recherche se fait de façon récursive                                                   | 3      |
| 09  | Il est possible d'annuler une tâche                                                       | 1      |
| 10  | Utilisation correcte des classes de progression                                           | 2      |
| 11  | Utilisabilité générale et robustesse de l'application                                     | 4      |
| 12  | Une logique UX est appliquée pour l'activation des boutons                                | 2      |
| 13  | Flexibilité et maintenance de l'architecture appliquée                                    | 2      |
| P   | Élément de code non conforme aux exigences vues dans le programme (par erreur)            | -0,5   |

### Bonus

| #   | Élément                                                                                   | Points |
| --- | ----------------------------------------------------------------------------------------- | ------ |
| 01  | Afficher l'emplacement (numéro de ligne et numéro de colonne) du résultat dans le fichier | 1      |
| 02  | Afficher un aperçu du résultat dans le fichier                                            | 1      |
| 03  | Afficher une progression réelle                                                           | 1      | -->

<hr><p align="Center"><img src="../../includes/end.png" alt="drawing" width="150"/></p> ```
