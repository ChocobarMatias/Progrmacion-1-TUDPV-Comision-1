# Reporte Técnico de Cátedra -- Fase 3

**Juego Asignado:** Variación Supervivencia -- Horda (DOOM 2D)\
**Número de Grupo:** Grupo 11

## 1. Integrantes del Equipo

-   **Cerri Marcelo Ezequiel** -- Legajo 67035. Desarrollo de
    `PlayerController`, `Disparar`, `Bala`, `Personajes`, `BarraVidaUI`
    y `CameraShake`.\
    Rama: `dev-MarceloEzequielCerri-67035-grupo11-semana3`.

-   **Brito Barón Jerónimo Ernesto** -- Legajo 67121. Desarrollo de
    `RecuperarSalud` y `RecuperarMunicion`.

-   **González Ana Sofía** -- Legajo 4948. Desarrollo de
    `EnemigoController`.\
    Rama: `dev-AnaSofíaGonzález-67128`.

-   **Carbonel Yalour José Tomás** -- Legajo 67086. Desarrollo de
    `Armas` y `SelectorArmasVisual`.\
    Rama: `dev-JoseTomasCarbonel-67086-grupo11-semana2`.

## 2. Diagnóstico y Arquitectura Inicial

El proyecto está estructurado mediante la separación de la lógica de
negocio, desarrollada en C#, y los componentes visuales e interactivos
proporcionados por Unity.

La clase base `Personajes` define los datos fundamentales relacionados
con la salud y el estado de los personajes. Los scripts
`PlayerController` y `EnemigoController` conectan la lógica del juego
con el motor físico de Unity (`Rigidbody2D`) y el sistema de colisiones
y triggers.

El sistema de combate se encuentra coordinado mediante los scripts
`Disparar` y `Bala`, encargados de gestionar los disparos y la
generación de proyectiles.

Por otra parte, el arsenal se administra mediante `Armas`, mientras que
`SelectorArmasVisual` representa gráficamente las armas seleccionadas en
el Canvas.

Finalmente, la interfaz y la retroalimentación del juego se complementan
con `BarraVidaUI`, `RecuperarSalud`, `RecuperarMunicion` y
`CameraShake`.

## 3. Módulos y Clases Implementadas

Durante esta fase se desarrollaron e integraron más de cuatro módulos,
cumpliendo con el requisito mínimo establecido por la cátedra.

-   **Clase Base: `Personajes`** (Desarrollada por Marcelo Ezequiel
    Cerri). Administra los atributos relacionados con la vida y el
    estado lógico de los personajes. Define constructores y métodos para
    gestionar la recepción y el cálculo del daño.

-   **Controlador del Jugador: `PlayerController`** (Desarrollado por
    Marcelo Ezequiel Cerri). Hereda de `MonoBehaviour` y coordina el
    movimiento del jugador, los períodos temporales de invulnerabilidad
    mediante corrutinas, el parpadeo del `SpriteRenderer`, la
    reproducción de efectos de sonido mediante
    `AudioSource.PlayOneShot()` y la comunicación con `CameraShake`.

-   **Controlador de Enemigos: `EnemigoController`** (Desarrollado por
    Ana Sofía González). Hereda de `MonoBehaviour` y gestiona el
    comportamiento de los enemigos dentro del escenario, incluyendo la
    aplicación de daño al jugador mediante el contacto o los triggers.

-   **Gestor de Armas: `Armas`** (Desarrollado por José Tomás Carbonel).
    Define y administra las propiedades de las armas disponibles, como
    el daño, la cadencia de disparo y el tipo de munición. También
    controla la selección del arma activa.

-   **Sistema de Disparo y Proyectiles: `Disparar` y `Bala`**
    (Desarrollados por Marcelo Ezequiel Cerri). `Disparar` controla la
    cadencia de los disparos y la instanciación dinámica de proyectiles
    durante la ejecución. Por su parte, `Bala` administra el
    desplazamiento físico mediante `linearVelocity` y la destrucción del
    proyectil utilizando `Destroy()`.

-   **Interfaz y Efectos Visuales: `BarraVidaUI` y `CameraShake`**
    (Desarrollados por Marcelo Ezequiel Cerri). `BarraVidaUI` actualiza
    los componentes `TextMeshProUGUI` y `Slider` para mostrar la
    información de salud del jugador. `CameraShake` implementa una
    corrutina que produce una sacudida de cámara cuando el jugador
    recibe daño.

-   **Sistema de Objetos Consumibles: `RecuperarSalud` y
    `RecuperarMunicion`** (Desarrollados por Brito Barón Jerónimo
    Ernesto). Gestionan los objetos recogibles del escenario,
    permitiendo recuperar puntos de salud y munición cuando el jugador
    entra en contacto con ellos.

-   **Selector Visual de Armas: `SelectorArmasVisual`** (Desarrollado
    por José Tomás Carbonel). Vincula el estado del sistema `Armas` con
    su representación gráfica en la interfaz del Canvas, permitiendo
    visualizar el arma seleccionada.

## 4. Control de Errores y Validaciones

Durante el desarrollo se contemplaron diferentes mecanismos de
validación para mejorar la estabilidad del proyecto:

-   **Comprobación de referencias nulas:** En los controladores del
    jugador y los enemigos se realizan verificaciones antes de utilizar
    referencias a componentes externos, como sistemas de audio,
    elementos de interfaz y efectos de cámara.

-   **Encapsulamiento:** Se utilizan atributos privados y mecanismos de
    control de acceso para proteger los datos internos de las clases y
    evitar modificaciones incorrectas de los valores de salud.

-   **Control de invulnerabilidad:** Se utilizan variables booleanas
    para administrar los períodos temporales de invulnerabilidad y
    evitar que el jugador reciba daño repetidamente durante el mismo
    intervalo.

-   **Administración de proyectiles:** Los objetos `Bala` se destruyen
    al finalizar su función para evitar la acumulación innecesaria de
    instancias durante la partida.

## 5. Guía de Ejecución y Controles (Cómo Jugar)

1.  **Movimiento:** Utilizar las teclas `W`, `A`, `S` y `D` para
    desplazarse por el escenario.
2.  **Selección de armas:** Utilizar las teclas numéricas `1`, `2`, `3`
    y `4` para seleccionar las diferentes armas disponibles.
3.  **Disparo:** Utilizar el clic izquierdo del mouse para disparar
    proyectiles.
4.  **Recepción de daño:** Entrar en contacto con un enemigo para
    comprobar la reducción de salud, la reproducción de efectos de
    sonido, el período de invulnerabilidad y la sacudida de cámara.
5.  **Recolección de objetos:** Recoger los objetos disponibles en el
    escenario para comprobar el funcionamiento de los sistemas de
    recuperación de salud y munición.

## 6. Conclusión

En esta fase se logró desarrollar e integrar los principales sistemas
necesarios para el funcionamiento de la variación de supervivencia por
hordas del videojuego DOOM 2D.

Se trabajó en la implementación de los sistemas de movimiento, combate,
enemigos, armas, proyectiles, recuperación de recursos e interfaz
gráfica.

La división de tareas entre los integrantes permitió desarrollar los
distintos módulos de manera organizada, facilitando posteriormente su
integración dentro del proyecto de Unity.

De esta manera, el Grupo 11 completó una estructura funcional que reúne
los componentes principales del videojuego y cumple con los objetivos de
desarrollo establecidos para esta fase.
