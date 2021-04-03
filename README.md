# Be faster

## Contexte de l'application
L’application Be-faster est un jeu mobile dont le but est de contrôler une voiture grâce à l'accéléromètre du téléphone afin d'éviter les trafic présent sur la route.

Une notion de score sera présente sur l’application, le joueur devra donc aller le plus vite possible sans se provoquer un accident, cet accident se déclenche quand la voiture contrôlée entre en collision avec une autre voiture.

Le joueur pourra accélérer tout en maintenant l’appuie sur l’écran et décéléré en relâchant l’écran, c’est au joueur de gérer sa vitesse afin d'éviter le maximum d’accident.


## Developpement de l'application

Pour le developpement de l'application, nous avons choisi le framework Monogame pour créer un jeu en 2D sur smartphone.
L'application est codé en C#.
Pour ce qui est de la conception de l'application, nous avons deux solutions. 
- La premiere `"Be Faster"` est une solution qui permet de faire le corps de l'applicaiton, c'est cette solution qui va permettre faire les traitement avec le métier comme, gérer le déplacemnet de la voiture du joueur ainsi que les différents spawn de toutes les autres voitures.
- La deuxième `"BeFaster"` est une solution qui permet d'appliquer la première solution mais pour les appariels Android

Grace à cette archtecture, nous pourrons par la suite, deployer le jeu sur différentes plateformes comme par exemple sur Bureau , IOS, lunux et bien d'autres encore.




