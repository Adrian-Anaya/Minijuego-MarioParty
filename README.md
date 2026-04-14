## Minijuego-MarioParty

## Descripción
Este es un minijuego de supervivencia y agilidad desarrollado en **Unity 2D**. El jugador debe enfrentarse a una lluvia aleatoria de enemigos tipo **Thwomp** que caen del techo. El desafío consiste en esquivar los ataques y mantenerse con vida hasta que el cronómetro llegue a cero.

## Instrucciones de Juego
* **Objetivo:** Sobrevivir durante **60 segundos**.
* **Movimiento:** Usa las teclas `A` - `D` o las **flechas direccionales** para moverte a los lados.
* **Salto:** Presiona la tecla `Espacio` para saltar y evitar colisiones accidentales.
* **Condición de Victoria:** El tiempo llega a 0 y al menos un jugador sigue en pie.
* **Condición de Derrota:** Todos los jugadores en la escena son aplastados por los Thwomps.

## Características Técnicas
* **GameManager:** Controla el tiempo de juego, la generación aleatoria de ataques y el estado de victoria/derrota.
* **Sistema de Animación:** Configuración completa de estados (*Idle, Run, Jump, Death*) con transiciones fluidas.
* **Detección de Colisiones:** Uso de *Kinematic Contacts* para asegurar que los enemigos detecten correctamente al jugador durante el ataque.
* **Soporte Multijugador Local:** El sistema gestionara múltiples personajes simultáneamente a futuro.

## Instalación
1. Clona este repositorio.
2. Abre el proyecto en **Unity** (versión 6000.0.60f1).
3. Carga la escena `SampleScene.unity` ubicada en `Assets/Scenes/`.
4. Dale al botón de **Play**
