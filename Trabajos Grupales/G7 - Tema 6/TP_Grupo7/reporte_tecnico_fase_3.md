# Reporte Técnico de Cátedra - Fase 3: Game Loop Completo, Prefabs Dinámicos, UI, Corrutinas y Game Juice

**Juego Asignado:** Minecraft 2D (Tema 6)  
**Número de Grupo:** Grupo 7  
**Fecha de Entrega:** Lunes 9 de Noviembre de 2026  

---

## 1. Integrantes del Equipo

* Integrante 1 - Legajo 67068 (Rol / Rama: dev-MatiasBezdjianBumbacher-67068)
* Integrante 2 - Legajo 67120 (Rol / Rama: dev-EnricoDuranNadia-67120)
* Integrante 3 - Legajo 67067 (Rol / Rama: dev-RicardoSilvaTeseira-67067)

---

## 2. Auditoría del Código Heredado y Refactorización (Fase 2 -> Fase 3)

Al asumir el proyecto heredado de la Fase 2, se realizó una auditoría del código y la arquitectura visual:

1. **Estado del Modelo POO (`TP_Grupo6` / `TP_Grupo7`):**
   * Se verificó que las clases puras en C# (`Bloque`, `Herramienta`, `PicoHierro`, `PicoDiamante`, `Inventario`) mantuvieran el desacoplamiento estricto sin heredar de `MonoBehaviour`.
   * **Refactorización Técnica:** Se extendió la clase base `Herramienta.cs` agregando la propiedad `DurabilidadMaxima` con encapsulamiento adecuado (`get; protected set;`). Esto permitió sincronizar dinámicamente el límite del `Slider` en el UI Canvas sin vulnerar las reglas del modelo.

2. **Limpieza de Deudas Técnicas y Convenciones de C# Moderno:**
   * Se aseguró el uso exclusivo de `rb.linearVelocity` en el script `MovimientoJugador.cs`, descartando la propiedad obsoleta `rb.velocity`.
   * Se implementó el control de destrucción temporal mediante `Destroy(gameObject, tiempo)` en los prefabs dinámicos de los drops para evitar fugas de memoria RAM en ejecuciones prolongadas.
   * Se migró la gestión de texto visual a `TextMeshProUGUI` (`using TMPro;`), eliminando por completo referencias al componente `UnityEngine.UI.Text`.

---

## 3. Arquitectura y Módulos Desarrollados en la Fase 3

### A. Corrutinas y Manejo de Tiempo (`IEnumerator` y `yield return`)

* **Minado Continuo (`MineriaJugador.cs`):** 
  En cumplimiento con las consignas específicas del **Tema 6 (Minecraft 2D)**, se implementó la corrutina `RutinaMinadoContinuo()`. Al mantener presionado el clic izquierdo (`Input.GetMouseButtonDown(0)`), se ejecuta un bucle temporal no bloqueante que aplica golpes al bloque apuntado cada `0.4` segundos, actualizando su desgaste hasta romperse y deteniendo el proceso al soltar el botón (`Input.GetMouseButtonUp(0)`).

* **Cuadro de Invulnerabilidad y Parpadeo Visual (`JugadorSaludPuente.cs`):** 
  Al recibir daño, el personaje activa la corrutina `RutinaInvulnerabilidad(1.5f)`. Durante 1.5 segundos, el jugador es inmune a nuevos impactos mientras su `SpriteRenderer` parpadea (alternando el valor alpha de color entre `0.2f` y `1.0f` cada `0.1s`), retornando a la opacidad total al finalizar.

* **Transición de Derrota:** 
  Al llegar la salud a cero, la corrutina `RutinaMuerte()` desactiva los controles, muestra el panel de Game Over y espera 2 segundos antes de reiniciar el nivel.

### B. Prefabs y Spawning Dinámico

* **Prefabs de Drops (`DropBloque.cs`):** 
  Al picar y romper la dureza total de un bloque, `MineriaJugador.cs` destruye el GameObject del bloque e instancia dinámicamente el Prefab `Drop` en las coordenadas exactas del bloque destruido (`Instantiate(prefabDrop, posicion, Quaternion.identity)`). El drop generado posee físicas de caída, un temporizador de autodestrucción tras 30 segundos y la lógica para transferir su ítem al `InventarioJugador`.

### C. Interfaz de Usuario 2D (UI Canvas con TMPro)

* **`UIController.cs`**: Controlador central encargado de recibir los eventos del modelo y actualizar los elementos gráficos:
  * **Salud del Jugador:** Visualizada en un `Slider` vinculado en tiempo real a la clase de modelo `SaludJugador`.
  * **Durabilidad de Herramienta:** Visualizada mediante un `Slider` secundario que refleja la relación `Durabilidad / DurabilidadMaxima` del pico equipado.
  * **Inventario Dinámico:** Componente `TextMeshProUGUI` que muestra en pantalla la cantidad de unidades recolectadas de cada material (`Piedra`, `Diamante`, `Obsidiana`).

### D. Game Juice, Audio y Control de Escenas

* **Feedback Sonoro (`AudioSource.PlayOneShot`):** 
  Se integraron efectos de audio independientes para las acciones clave: impacto de minado (`sonidoPicar`), rotura de bloque (`sonidoRomper`), recolección de drop (`sonidoRecoger`) y daño al jugador (`sonidoDanio`).
* **Temblor de Cámara (`CameraShake.cs`):** 
  Sistema singleton de sacudida responsiva que desplaza aleatoriamente la cámara principal en un radio determinado durante impactos ligeros (minado) e impactos fuertes (recibir daño / romper bloque).
* **Gestión de Reinicio (`SceneManager`):** 
  Al agotarse la salud del jugador (`Modelo.EstaMuerto == true`), se ejecuta el reinicio automático de la escena activa mediante `SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)`.

---

## 4. Guía de Verificación y Pruebas en Escena (`EscenaMinecraft`)

Para evaluar el correcto funcionamiento de la entrega en Unity:

1. **Inicio y Visualización de UI:**
   * Al hacer clic en **Play**, verificar que el Canvas muestre los dos `Sliders` (Salud completa y Durabilidad del `PicoHierro`) y el panel de texto con el inventario en 0.

2. **Minado Continuo y Desgaste:**
   * Mantener presionado el **clic izquierdo** apuntando a un bloque de `Piedra` dentro del rango (3 unidades). Se observará y escuchará la secuencia de picado continuo cada 0.4s con temblor leve de cámara.
   * Verificar cómo el `Slider` de durabilidad del pico se reduce gradualmente.

3. **Instanciación y Recolección de Drops:**
   * Al romperse el bloque, se escuchará el sonido de rotura, la cámara temblará fuertemente, el bloque desaparecerá y se instanciará el **Prefab Drop** en su lugar.
   * Caminar hacia el Drop. Al entrar en contacto trigger, se reproducirá el sonido de recolección y el texto de `TextMeshProUGUI` sumará +1 al ítem correspondiente.

4. **Cambio de Herramienta:**
   * Presionar la tecla **`2`** para equipar el `PicoDiamante`. El `Slider` de durabilidad se actualizará automáticamente a los valores del nuevo pico.

5. **Invulnerabilidad y Game Over (Muerte):**
   * Al recibir daño, el personaje comenzará a parpadea de forma semi-transparente durante 1.5 segundos, siendo inmune a golpes consecutivos.
   * Si la salud del jugador llega a 0, la interfaz desplegará el panel de derrota y la escena se recargará automáticamente tras 2 segundos de espera.