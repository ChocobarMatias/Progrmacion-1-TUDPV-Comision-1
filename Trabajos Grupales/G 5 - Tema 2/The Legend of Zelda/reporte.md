# [ENTREGA FASE 3] Grupo 05 - Tema: The Legend of Zelda 2D

## 1. Integrantes

El grupo está formado por dos integrantes. Matías Daniel Monteros, legajo 140911, trabajó en la rama dev-MatiasMonteros-140911. Luz Daniela Lezcano, legajo 67119, trabajó en la rama dev-LuzLezcano-67119.

## 2. Auditoría del código heredado (Semana 2)

Al abrir el proyecto que recibimos del grupo anterior, lo primero que apareció fueron errores de entorno y no de código. Unity mostraba un DirectoryNotFoundException dentro de Library/PackageCache porque la ruta donde estaba guardado el repositorio superaba el límite de 260 caracteres de Windows. Lo solucionamos moviendo el repositorio a una ruta más corta y borrando la carpeta Library para que Unity la regenerara. También aparecía una advertencia avisando que el Input Manager está deprecado. El proyecto usa el sistema de input clásico (Input.GetAxisRaw e Input.GetKeyDown), y decidimos mantenerlo porque todo el código heredado depende de él y cambiarlo habría roto los scripts.

Después revisamos el código y encontramos varias deudas técnicas que corregimos.

La más importante estaba en Enemigo.cs. Toda la lógica de vida y daño del enemigo vivía dentro de una clase que heredaba de MonoBehaviour, lo que va en contra de la regla de no tener "God Classes". La separamos en dos partes: EnemigoModelo, una clase pura de C# que se crea con new dentro de Awake, y Enemigo, que queda solo como puente con Unity. Mantuvimos los mismos métodos públicos para no romper el script de la espada.

También notamos que ningún enemigo le hacía daño a Link, así que era imposible perder la partida. Agregamos daño por contacto: cuando un enemigo toca a Link, llama a RecibirGolpe del PlayerController.

En Cofre.cs, si el cofre no tenía un Animator, el script mostraba una advertencia pero igual intentaba ejecutar la animación de apertura, lo que provocaba un NullReferenceException. Agregamos una verificación para que solo la ejecute si el Animator existe.

En PlayerController.cs encontramos tres problemas. El primero es que la propiedad DanioEspada devolvía el daño completo aunque la Espada Maestra estuviera rota, así que una espada con durabilidad cero seguía pegando igual; ahora en ese caso devuelve cero. El segundo es un comentario engañoso en FixedUpdate que decía que se usaba rb.velocity en lugar de linearVelocity, cuando en realidad el código usaba linearVelocity; lo eliminamos y confirmamos que no queda ningún uso de rb.velocity en el proyecto. El tercero es que, al atacar, la hitbox de la espada solo se movía hacia donde miraba Link pero nunca se rotaba, así que al atacar hacia arriba o hacia abajo la espada quedaba horizontal y el área de daño no coincidía con la dirección del ataque. Ahora la hitbox se rota usando Mathf.Atan2 con la dirección en la que mira el personaje.

En SwordHitbox.cs, el script dependía de que alguien le asignara a mano la referencia al jugador en el Inspector, y si quedaba vacía se producía un NullReferenceException. Ahora, si no está asignada, la busca automáticamente en el objeto padre. Además, en vez de depender solo del Tag "Enemigo", detecta al enemigo por su componente.

En ItemZelda.cs, el constructor no validaba el nombre ni la durabilidad del ítem, a diferencia de la clase Link, que sí validaba sus datos. Agregamos esas validaciones para que el comportamiento sea consistente.

Por último, la clase Link no tenía ninguna forma de avisar cuándo cambiaba la salud, lo que hacía difícil conectarla con la interfaz. Le agregamos el evento SaludCambiada, que se dispara cada vez que cambian los corazones y al que se suscribe la UI.

Detectamos además que el Arco de Fuerza se equipa en el inventario pero ningún control lo usa. Lo dejamos como estaba porque no forma parte de lo que pide la Fase 3.

## 3. Corrutinas y sistema de Prefabs

El Prefab que instanciamos es el rayo de espada, una mecánica clásica de Zelda. Cuando el jugador presiona Espacio, Link ataca con la espada, y si en ese momento tiene la salud al máximo y la espada no está rota, además dispara un rayo. Para eso usamos un objeto vacío hijo de Link llamado firePoint, que se coloca hacia donde está mirando el personaje, y desde esa posición se crea el rayo con Instantiate. El rayo se mueve en línea recta asignándole linearVelocity a su Rigidbody2D y se rota para apuntar en la dirección del disparo. Cuando choca con una pared o con un enemigo, reproduce un sonido de impacto, le aplica al enemigo el daño de la Espada Maestra y se destruye. Para no dejar basura en memoria, en su método Start se programa Destroy(gameObject, tiempoVidaMaximo), así que si no choca con nada igual desaparece a los dos segundos.

En cuanto a las corrutinas, la principal es la de invulnerabilidad. Cuando Link recibe un golpe, durante 1.5 segundos queda invulnerable y su sprite parpadea, alternando entre casi transparente y opaco cada 0.1 segundos con WaitForSeconds(0.1f). Mientras dura ese tiempo, cualquier golpe nuevo se ignora, lo que evita recibir daño doble al instante. Todo esto ocurre sin congelar el juego.

Hay otras corrutinas que también manejan el tiempo. El ataque con espada, que ya venía de la Semana 2, mantiene activa la hitbox durante 0.2 segundos. Agregamos una alerta de vida baja: cuando Link queda con el 30% de la vida o menos, es decir 3 de 10 corazones, una corrutina repite un sonido de aviso cada 0.8 segundos, como en el Zelda original. Esta alerta se activa y se apaga sola escuchando el evento SaludCambiada, y se detiene cuando Link muere. El temblor de cámara también es una corrutina: desplaza la cámara al azar durante 0.2 segundos y después la devuelve a su lugar. Por último, cuando Link muere, el GameManager inicia una corrutina que espera 2 segundos y recarga la escena con SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex).

Para el audio usamos PlayOneShot. Link tiene su propio AudioSource, desde el que suenan el ataque con espada, el disparo del rayo, el golpe recibido y la alerta de vida baja. El ataque con espada tiene cuatro variantes de sonido, y en cada golpe se elige una al azar sin repetir la misma dos veces seguidas, para que no suene repetitivo. Los sonidos de objetos que se destruyen, como el impacto del rayo, la muerte de un enemigo, el cofre o la derrota, se reproducen desde un AudioSource del GameManager, porque si sonaran desde el propio objeto el sonido se cortaría en el momento en que se destruye.

Sobre las estructuras de datos, usamos dos arreglos fijos y una lista dinámica. El primer arreglo es Transform[] puntosPatrulla, con los tres puntos por los que patrulla el esqueleto Stalfos, que los recorre en orden del punto A al B, después al C y vuelve a empezar. El segundo es AudioClip[] sonidosEspada, que guarda las cuatro variantes del sonido de ataque. La lista dinámica es List<Enemigo> enemigosActivos en el GameManager: cada enemigo se agrega a la lista cuando aparece y se quita con Remove cuando muere, y la cantidad restante se muestra en pantalla.

## 4. UI configurada (Canvas)

La interfaz está en un Canvas con tres elementos. Arriba a la izquierda hay un Slider con relleno rojo que representa la vida de Link. Le sacamos el handle para que no se pueda arrastrar, y su valor máximo es la cantidad máxima de corazones del modelo. Se actualiza en tiempo real porque el script HUD está suscripto al evento SaludCambiada de la clase Link. Debajo del slider hay un texto con TextMeshProUGUI que muestra la vida actual, la cantidad de enemigos que quedan y si el rayo de espada está disponible: dice LISTO en verde cuando Link tiene la vida llena y BLOQUEADO en rojo cuando no. El tercer elemento es un panel negro semitransparente con el texto GAME OVER, que permanece oculto y se muestra cuando Link muere.

En la primera captura se ve la interfaz durante la partida, con el slider lleno, la vida en 10 de 10, un enemigo en la sala y el rayo de espada listo.

![UI durante la partida](capturas/ui-partida.png)

En la segunda captura se ve el momento de la derrota: el slider vacío, la vida en 0 de 10, el rayo bloqueado y el cartel de GAME OVER sobre la escena, justo antes de que el nivel se reinicie.

![UI en Game Over](capturas/ui-game-over.png)

La mazmorra está armada con dos Tilemaps. Uno es el piso, que es solo visual, y el otro son las paredes, que tienen un Tilemap Collider 2D para que Link y el rayo choquen contra ellas. Los sprites de la mazmorra y del Stalfos son del pack Tiny Dungeon de Kenney (kenney.nl), que tiene licencia CC0 de uso libre. Todos los sprites se importaron como pixel art, con 16 píxeles por unidad, filtro Point y sin compresión, para que se vean nítidos.

## 5. Guía de prueba para el docente

Para probar el juego hay que abrir la escena Juego y presionar Play. Link se mueve con WASD o con las flechas.

Al presionar Espacio varias veces, Link ataca con la espada y en cada golpe suena una variante distinta del sonido de ataque. La espada apunta siempre hacia donde mira el personaje. Como Link empieza con la vida llena, cada ataque también dispara el rayo de espada, que viaja en línea recta, suena al chocar contra una pared o un enemigo y desaparece.

Mientras tanto se puede ver al esqueleto Stalfos patrullando la sala, pasando en orden por los puntos A, B y C.

Si Link se deja tocar por el Stalfos, recibe daño: su sprite parpadea, suena el golpe, la cámara tiembla, el slider de vida baja y el texto del rayo pasa a BLOQUEADO. Durante el parpadeo no recibe golpes nuevos. A partir de ahí, al presionar Espacio solo ataca con la espada, sin rayo, porque ya no tiene la vida al máximo. Si sigue recibiendo daño hasta quedar con 3 corazones o menos, empieza a sonar en forma repetida la alerta de vida baja.

Al matar al Stalfos con la espada, el contador de enemigos del texto baja. Acercándose al cofre y presionando E, Link obtiene la Llave pequeña.

Finalmente, si Link pierde todos los corazones, aparece el cartel de GAME OVER y a los 2 segundos la escena se reinicia sola.
