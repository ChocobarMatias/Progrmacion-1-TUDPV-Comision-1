# Reporte Técnico de Cátedra - Fase 2
**Juego Asignado:** Minecraft 2D (Tema 6)  
**Número de Grupo:** Grupo 6  

---

## 1. Integrantes del Equipo
* Integrante 1 - Legajo 67068 (Rol / Rama: dev-MatiasBezdjianBumbacher-67068)
* Integrante 2 - Legajo 67120 (Rol / Rama: dev-EnricoDuranNadia-67120)
* Integrante 3 - Legajo 67067 (Rol / Rama: dev-RicardoSilvaTeseira-67067)

---

## 2. Diagnóstico y Arquitectura de Integración (Unity 2D)
En esta segunda fase se conectó la arquitectura orientada a objetos (POO) en C# desarrollada para la consola en la Fase 1 con el motor de juego Unity 2D, respetando estrictamente el principio de separación de responsabilidades:

* **Dominio / Modelo C# Puro (`Assets/Scripts/Modelo/`):** Las clases del modelo original (`Bloque`, `Herramienta`, `PicoHierro`, `PicoDiamante`) no fueron modificadas y se incorporó `Inventario.cs`. Ninguna de estas clases hereda de `MonoBehaviour`, lo que garantiza que la lógica central permanezca desacoplada del motor visual.
* **Capa Puente (`Assets/Scripts/Puentes/`):** Componentes `MonoBehaviour` encargados de vincular los eventos de la escena (física, inputs de usuario, ciclo de vida del juego) con el modelo lógico. Cada script puente encapsula una instancia o referencia de su modelo correspondiente.
* **Separación de Responsabilidades:** Se evitó la creación de *God Classes*. La física, el movimiento, el salto, la minería, los componentes de inventario y la lógica de bloques fueron separados en scripts específicos con una única responsabilidad.

---

## 3. Módulos y Clases Implementadas en Unity

### A. Capa Puente (Conexión Motor - Modelo)
* **`BloqueJuego.cs`**: Asociado a cada objeto de bloque en la escena. Instancia un objeto del modelo C# `Bloque` en su método `Awake()`, tomando los valores iniciales del Inspector.
* **`MineriaJugador.cs`**: Gestiona la interacción de minado mediante `Physics2D.RaycastAll` desde la posición del jugador hacia el puntero del mouse. Llama internamente a los métodos del modelo (`Herramienta.PicarBloque`) para procesar el daño y, cuando el modelo notifica que el bloque está roto (`EstaRoto`), destruye el objeto visual e instancia el *drop*.
* **`InventarioJugador.cs`**: Contiene la referencia a la instancia del modelo `Inventario`. Expone la interfaz necesaria para que otros objetos interactúen con el inventario del jugador.
* **`DropBloque.cs`**: Administra la entidad física recolectable en el mundo de juego. Detecta la colisión con el jugador mediante `OnTriggerEnter2D` y añade el ítem al `InventarioJugador`.

### B. Controladores de Física y Jugador
* **`MovimientoJugador.cs`**: Controla el movimiento horizontal del jugador aplicando velocidades al componente `Rigidbody2D` (`rb.linearVelocity`) dentro de `FixedUpdate()`, conservando la velocidad vertical de caída.
* **`SaltoJugador.cs`**: Procesa la intención de salto en `Update()` y ejecuta el impulso de física (`rb.AddForce` con `ForceMode2D.Impulse`) en `FixedUpdate()`. Determina si el jugador está verdaderamente sobre una superficie horizontal mediante la evaluación de las normales de contacto (`contact.normal.y > 0.6f`) en `OnCollisionStay2D`.

### C. Nuevas Clases del Modelo
* **`Inventario.cs`**: Clase C# pura encargada de registrar la cantidad de ítems recolectados utilizando una estructura de datos `Dictionary<string, int>`.

---

## 4. Buenas Prácticas y Cumplimiento de Reglas Físicas

1. **Captura e Integración de Inputs y Física (Regla 3):**
   * La detección de teclas y botones del mouse (`Input.GetAxisRaw`, `Input.GetButtonDown`, `Input.GetKeyDown`) se realiza exclusivamente en el método `Update()`.
   * La aplicación de movimiento físico y manipulación directa de `Rigidbody2D` se ejecuta estrictamente dentro del método `FixedUpdate()`.

2. **Manipulación Física Correcta (Regla 2):**
   * No se altera `transform.position` para objetos con dynamic physics. El movimiento del personaje utiliza `rb.linearVelocity` (o `rb.velocity` según la versión de Unity) y el salto utiliza impulsos instantáneos (`AddForce`).

3. **Restricción de Rotación Física (Regla 4):**
   * Se aplicó la restricción `Freeze Rotation Z` en los componentes `Rigidbody2D` del jugador y los *drops* para prevenir rotaciones accidentales por fricción o colisión.

4. **Validación de Superficies de Suelo (Regla 5):**
   * El estado `enSuelo` no depende de la colisión simple de bounding boxes, sino de la iteración sobre los puntos de contacto en `OnCollisionStay2D`, verificando que la normal vertical (`normal.y > 0.6f`) garantice un apoyo real desde la parte inferior.

5. **Materiales Físicos y Control de Raycast:**
   * Se utilizó un `Physics Material 2D` denominado `SinFriccion` (Fricción = 0, Bounciness = 0) para evitar que el personaje se adhiera lateralmente a los paredones o bloques al saltar.
   * `MineriaJugador` implementa `Physics2D.RaycastAll` filtrando los colisionadores pertenecientes al propio jugador o a entidades tipo *drop* para garantizar un minado preciso.

---

## 5. Guía de Verificación y Pruebas en Escena (`EscenaMinecraft`)

 Para evaluar el funcionamiento del sistema en el entorno de desarrollo:

1. **Carga e Inicio:**
   * Abrir la escena `EscenaMinecraft` y reproducir la simulación (Play).
   * Verificar en la **Console** (Ctrl + Shift + C) los mensajes iniciales `[BloqueJuego]` confirmando la instanciación de los modelos y el mensaje de log indicando la herramienta inicial (`PicoHierro`).

2. **Movimiento y Salto:**
   * Utilizar las teclas `A` / `D` o las flechas de dirección para desplazar al personaje lateralmente.
   * Presionar la barra espaciadora (`Space`) para validar el salto y corroborar que no exista trabado vertical en las paredes de los bloques.

3. **Mecánica de Minería y Polimorfismo:**
   * Con el `PicoHierro` seleccionado por defecto (Tecla 1), realizar clic izquierdo sobre el bloque de **Piedra**: la dureza disminuirá progresivamente hasta romperse, destruyendo el bloque e instanciando un objeto *drop*.
   * Intentar picar el bloque de **Obsidiana** con el `PicoHierro`: la consola registrará que la herramienta no le hace daño (regla de negocio del modelo).
   * Cambiar al `PicoDiamante` presionando la tecla **2** y volver a picar la Obsidiana: el bloque registrará el daño correspondiente y se romperá.

4. **Recolección e Inventario:**
   * Desplazar al jugador hacia la posición del *drop* generado tras romper un bloque.
   * Observar en la consola el mensaje `[Inventario] +1 [Tipo]` y la consecuente destrucción del objeto *drop* en la escena visual.