# Programación 1 - TUDPV (Comisión 1)
**Tecnicatura Universitaria en Diseño y Programación de Videojuegos**  
**Docente:** Profesor Matías Chocobar  
**Repositorio Oficial:** [https://github.com/ChocobarMatias/Progrmacion-1-TUDPV-Comision-1](https://github.com/ChocobarMatias/Progrmacion-1-TUDPV-Comision-1)

---

> [!CAUTION]
> ### ⚠️ REGLA CRÍTICA DE ENTREGA
> **¡ATENCIÓN OBLIGATORIA PARA TODOS LOS ALUMNOS!**  
> **NO CLONAR DIRECTAMENTE** el repositorio del Profesor Matías Chocobar.  
> Es **OBLIGATORIO** hacer primero un **FORK** hacia sus cuentas individuales de GitHub. Quien intente clonar el repositorio original sin realizar el fork no poseerá permisos de escritura, no podrá subir sus cambios y la entrega quedará automáticamente **anulada**.
> Es **OBLIGATORIO IMPORTANTE** crear dentro de la carpeta llamda Trabajos de Alumnos un Carpeta con el Legajo-Apellido y Nombre del Alumno donde colocara su ejercicio resuelto, no se aceptaran trabajos que no esten en su respectiva carpeta. 

---

## 📌 Guía Técnica Paso a Paso: Flujo de Entrega con GitHub Desktop

Siga detalladamente cada uno de los pasos descritos a continuación para la correcta resolución y entrega del Trabajo Práctico:

### Paso 1: Crear el Fork
1. Ingrese a su cuenta de GitHub y acceda al [repositorio oficial de la cátedra](https://github.com/ChocobarMatias/Progrmacion-1-TUDPV-Comision-1).
2. En el margen superior derecho, haga clic en el botón **Fork** y seleccione **Create a new fork**.
3. Esto generará una copia íntegra del proyecto en su cuenta personal bajo la estructura:  
   `https://github.com/<tu_usuario>/Progrmacion-1-TUDPV-Comision-1`

### Paso 2: Clonar el Fork con GitHub Desktop
1. Abra la aplicación **GitHub Desktop**.
2. Diríjase a **File > Clone Repository...** (o presione `Ctrl + Shift + O`).
3. En la pestaña **GitHub.com**, seleccione **su propio Fork** (verifique que el autor sea su usuario y no el del profesor).
4. Indique la ruta local en su disco donde trabajará y presione **Clone**.

### Paso 3: Crear y publicar la rama de desarrollo
1. En GitHub Desktop, haga clic en el selector superior **Current branch** y luego en **New branch**.
2. **Nomenclatura obligatoria:** `dev-NombreApellido-Legajo`  
   *Ejemplo:* `dev-JuanPerez-54321`
3. Haga clic en **Create branch** y posteriormente en el botón azul **Publish branch** para sincronizar la rama en su repositorio remoto.  
   > *Equivalente en terminal de Git:*  
   > `git checkout -b dev-NombreApellido-Legajo`

### Paso 4: Resolver el Trabajo Práctico y registrar cambios (Commit)
1. Abra la solución del proyecto en **Visual Studio Community**, resuelva las consignas solicitadas y guarde todos los archivos (`Ctrl + S`).
2. Regrese a GitHub Desktop y verifique los cambios detectados en la pestaña lateral **Changes**.
3. En la esquina inferior izquierda, complete el campo **Summary**:  
   `TP Integrador - Resueltos Ejercicios 1 al 10`
4. Presione el botón **Commit to dev-NombreApellido-Legajo**.  
   > *Equivalente en terminal de Git:*  
   > `git add .`  
   > `git commit -m "TP Integrador - Resueltos Ejercicios 1 al 10"`

### Paso 5: Subir los cambios al repositorio remoto (Push)
1. En GitHub Desktop, presione el botón superior derecho **Push origin** para subir los commits confirmados a su cuenta de GitHub.  
   > *Equivalente en terminal de Git:*  
   > `git push origin dev-NombreApellido-Legajo`

### Paso 6: Crear el Pull Request (PR) hacia el repositorio del Profesor
1. Tras realizar el *push*, presione **Create Pull Request** en GitHub Desktop (o acceda a su fork en la plataforma web de GitHub y seleccione **Contribute > Open pull request**).
2. Verifique la configuración del PR:
   * **base repository:** `ChocobarMatias/Progrmacion-1-TUDPV-Comision-1` (rama `main`)
   * **head repository:** `<tu_usuario>/Progrmacion-1-TUDPV-Comision-1` (rama `dev-NombreApellido-Legajo`)
3. **Título del Pull Request:**  
   `[ENTREGA TP] Nombre y Apellido - Legajo`
4. Haga clic en **Create pull request** para formalizar la entrega.

### Paso 7: Actualizar la rama main y sincronizar el Fork localmente
1. En GitHub Desktop, cambie a la rama principal: **Current branch: main**.
2. Diríjase a **Branch > Merge into current branch...**, elija su rama `dev-NombreApellido-Legajo` y confirme la fusión para mantener su rama `main` actualizada.  
   > *Equivalente en terminal de Git:*  
   > `git checkout main`  
   > `git merge dev-NombreApellido-Legajo`
3. En la interfaz web de su fork, cuando la cátedra incorpore nuevo material, utilice la opción **Sync fork > Update branch** para mantenerse alineado con el repositorio principal.

**Fecha Limite de entrega : Jueves 24/09/2026 a las 23:50 hs se cerrara los PR**
