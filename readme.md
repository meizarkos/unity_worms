# Worms-like Game (Unity Project)

## Concept du jeu

Ce projet est un jeu inspiré de *Worms* développé sous Unity.

Le gameplay repose sur :
- viser avec une arme
- tirer des projectiles
- infliger des dégâts aux ennemis
- gérer la position et la survie des personnages

Le joueur contrôle un personnage capable de changer d’angle, de puissance et de type d’arme afin d’éliminer les ennemis présents sur la carte.

Les assets proviennent de unity asset ou alors ont été généré par Claude

---

## Points techniques difficiles rencontrés

### Système de tir
Le système de tir repose sur :
- la direction de l’arme
- la puissance de tir
- l’angle de lancement

La transmission correcte de ces paramètres entre les scripts a été un point important.

---

### Visualisation du tir (mathématiques)
La trajectoire des projectiles est calculée et affichée à l’aide de formules physiques (gravité, vitesse initiale).

Cela a nécessité :
- l’utilisation de vecteurs
- des rotations de direction
- des calculs de trajectoire par étapes

---

## Points à améliorer

### Gameplay
- Ajouter plus de variété d’armes
- Améliorer les feedbacks (hit, explosions, UI)
- Rendre les combats plus dynamiques

---

### Architecture du code
- Le player centralise trop de responsabilités

---

## Améliorations futures possibles
- Système de tour par tour
- Effets visuels plus avancés (explosions, particules)
- IA ennemie plus intelligente
- Menu et progression de niveaux
---

## Conclusion

Ce projet sert de base pour un jeu de type *Worms* avec un système de tir physique et une logique de combat simple, mais extensible.