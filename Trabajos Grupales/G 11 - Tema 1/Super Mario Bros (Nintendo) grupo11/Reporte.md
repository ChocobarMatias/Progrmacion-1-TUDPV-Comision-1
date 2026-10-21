# Reporte Técnico de Cátedra - Fase 1

**Juego Asignado:** Super Mario Bros (Nintendo)
**Número de Grupo:** Grupo 11

## 1. Integrantes del Equipo

* Cerri, Marcelo Ezequiel - Legajo 67035 (Líder / Rama: `dev-MarceloEzequielCerri-67035-grupo-11`)
* González, Ana Sofía - Legajo 67128 (Rama: `dev-AnaSofíaGonzález-67128`)
* Carbonel Yalour, José Tomás - Legajo 67086 (Rama: `dev-JoseTomasCarbonelYalour-67086-grupo-11`)
* Brito Barón, Jerónimo Ernesto - Legajo 67121 (Colaborador en desarrollo de código)

## 2. Diagnóstico y Arquitectura Inicial

* La arquitectura se concibió bajo los principios de POO I y POO II mediante un diseño modular y jerárquico. Se estableció una clase base abstracta (`PersonajeMario`) para centralizar el comportamiento genérico y los atributos protegidos del sistema, sobre la cual se extienden clases hijas especializadas y una clase controladora de interacciones (`BloqueInterrogacion`), garantizando un bajo acoplamiento y alta cohesión.

## 3. Módulos y Clases Implementadas (Distribución de Tareas)

* **Clase Base `PersonajeMario`:** (Implementado por Marcelo Ezequiel Cerri) Clase abstracta que concentra los atributos protegidos (`nombre`, `vidas`, `monedas`), constructor parametrizado con `this`, método virtual `Saltar()`, método `RecibirDanio()` y propiedades con validación.
* **Clase Derivada `Mario`:** (Implementado por Ana Sofía González) Hereda de `PersonajeMario` usando `: base(...)`, incorpora el atributo privado `estadoTransformacion` y sobrescribe el método `Saltar()` mediante `override` para alcanzar mayor altura.
* **Clase Derivada `Luigi`:** (Implementado por José Tomás Carbonel Yalour) Hereda de `PersonajeMario` mediante `: base(...)` y sobrescribe el método `Saltar()` con su comportamiento característico de salto y menor tracción.
* **Clase Gestora `BloqueInterrogacion` y desarrollo de código:** (Implementado por Jerónimo Ernesto Brito Barón) Encapsula el estado booleano `abierto`, gestiona la entrega de recompensas con el método `Golpear` y colaboró activamente en la implementación del código del sistema.

## 4. Control de Errores y Validaciones

* Se aplicó encapsulamiento estricto utilizando modificadores de acceso `protected` y `private` para impedir modificaciones externas descontroladas sobre los datos críticos de los personajes.
* Mediante el uso de la propiedad `Vidas` con validación sobre la palabra clave `value`, se previno que el contador de salud descienda a números negativos. Si el valor ingresado es menor o igual a cero, se fija automáticamente en `0` y se activa de forma controlada la lógica de `[GAME OVER]`.
* En la clase gestora del bloque, se validó el estado interno booleano para prevenir un bug lógico que permitiría golpear un mismo bloque de recompensas de forma infinita.

## 5. Guía de Ejecución en Consola

Instrucciones para que el docente ejecute el `Main()` y verifique el polimorfismo:
1. Abrir la solución completa en **Visual Studio**.
2. Asegurarse de que el archivo de inicio sea `Program.cs` dentro del espacio de nombres `Grupo_11`.
3. Presionar el botón **Start** (o la tecla `F5`) para compilar y ejecutar la consola.
4. En la consola se visualizará de forma automática:
   * **Polimorfismo en bucle:** El recorrido del array polimórfico `PersonajeMario[] hermanos` ejecutando el método `Saltar()`, demostrando que cada clase hija responde de manera especializada.
   * **Gestión de Bloques:** La interacción en la que los personajes golpean el bloque y se valida correctamente su estado de apertura única.
   * **Validación de Vidas:** La simulación de daño consecutivo en Mario hasta agotar sus vidas, comprobando el disparo automático del sistema de `GameOver`.