using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System.Drawing;
using TGC.Core.Direct3D;
using TGC.Core.Example;
using TGC.Core.Geometry;
using TGC.Core.Input;
using TGC.Core.SceneLoader;
using TGC.Core.Textures;
using TGC.Core.Utils;
using TGC.Examples.Camara;
using System.Collections.Generic;
using System;
using TGC.Core.SkeletalAnimation;

namespace TGC.Group.Model
{
    /// <summary>
    ///     Ejemplo para implementar el TP.
    ///     Inicialmente puede ser renombrado o copiado para hacer más ejemplos chicos, en el caso de copiar para que se
    ///     ejecute el nuevo ejemplo deben cambiar el modelo que instancia GameForm <see cref="Form.GameForm.InitGraphics()" />
    ///     line 97.
    /// </summary>
    public class GameModel : TgcExample
    {
        private TgcScene sceneArbol, sceneMetralladoraFija, scenePasto, sceneFuenteAgua, scenePared, sceneCajaMunicion, sceneCamioneta, scenePatrullero;
        private TgcScene sceneMaquina, sceneBarril, sceneHelicoptero, sceneAvion, sceneHammer, sceneLogoTGC, sceneEscalera, sceneMetraMilitar;
        private TgcMesh pasto, arbol, fuenteAgua0, fuenteAgua1, fuenteAgua2, fuenteAgua3, metralladoraFija, pared, cajaMunicion, camioneta, patrullero, barril, helicoptero, avion, hammer, logo, maquina, escalera, metraMilitar;
        private List<TgcMesh> cesped, metralladoras, paredes, CajaMuniciones, patrulleros, barriles, aviones, maquinas, escaleras;
        private List<TgcMesh> pastos, arboles;
        private Matrix translateAux, translate, scale, rotation;
        private float angleY;
        private List<Vector3> posiciones,posPatrulleros,posBarriles,posParedes;
        private TgcScene scene0, scene1, scene2, scene3;

        /// <summary>
        ///     Constructor del juego.
        /// </summary>
        /// <param name="mediaDir">Ruta donde esta la carpeta con los assets</param>
        /// <param name="shadersDir">Ruta donde esta la carpeta con los shaders</param>
        public GameModel(string mediaDir, string shadersDir) : base(mediaDir, shadersDir)
        {
            Category = Game.Default.Category;
            Name = Game.Default.Name;
            Description = Game.Default.Description;
        }


        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo.
        ///     Escribir aquí todo el código de inicialización: cargar modelos, texturas, estructuras de optimización, todo
        ///     procesamiento que podemos pre calcular para nuestro juego.
        ///     Borrar el codigo ejemplo no utilizado.
        /// </summary>
        public override void Init()
        {
            //Definimos el loader
            TgcSceneLoader loader = new TgcSceneLoader();

            //Inicializamos la ciudad
            {
                var posicion = new Vector3(0, 0, 0);
                scene0 = loader.loadSceneFromFile(this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scene1 = loader.loadSceneFromFile(this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scene2 = loader.loadSceneFromFile(this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scene3 = loader.loadSceneFromFile(this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scale = Matrix.Scaling(new Vector3(1f, 1.8f, 1f));
                scene0.Meshes[0].AutoTransformEnable = scene0.Meshes[1].AutoTransformEnable = false;
                scene0.Meshes[0].Transform = scale;
                scene0.Meshes[1].Transform = scale;
                translate = Matrix.Translation(posicion);
                foreach (var mesh in scene0.Meshes) {
                    mesh.AutoTransformEnable = false;
                    mesh.Transform = translate;
                }
                scene1.Meshes[0].AutoTransformEnable = scene1.Meshes[1].AutoTransformEnable = false;
                scene1.Meshes[0].Transform = scale;
                scene1.Meshes[1].Transform = scale;
                posicion += new Vector3(0, 0, 2000);
                translate = Matrix.Translation(posicion);
                foreach (var mesh in scene1.Meshes) {
                    mesh.AutoTransformEnable = false;
                    mesh.Transform = translate;
                }
                scene1.Meshes.RemoveAt(5);
                scene1.Meshes.RemoveAt(6);
                scene1.Meshes.RemoveAt(7);
                scene1.Meshes.RemoveAt(18);
                scene2.Meshes[0].AutoTransformEnable = scene2.Meshes[1].AutoTransformEnable = false;
                scene2.Meshes[0].Transform = scale;
                scene2.Meshes[1].Transform = scale;
                posicion += new Vector3(-2000, 0, 0);
                translate = Matrix.Translation(posicion);
                foreach (var mesh in scene2.Meshes)
                {
                    mesh.AutoTransformEnable = false;
                    mesh.Transform = translate;
                }
                scene2.Meshes.RemoveAt(3);
                scene2.Meshes.RemoveAt(4);
                scene2.Meshes.RemoveAt(5);
                scene2.Meshes.RemoveAt(6);
                scene2.Meshes.RemoveAt(10);
                scene2.Meshes.RemoveAt(16);
                scene3.Meshes[0].AutoTransformEnable = scene3.Meshes[1].AutoTransformEnable = false;
                scene3.Meshes[0].Transform = scale;
                scene3.Meshes[1].Transform = scale;
                posicion += new Vector3(0, 0, -2000);
                translate = Matrix.Translation(posicion);
                foreach (var mesh in scene3.Meshes)
                {
                    mesh.AutoTransformEnable = false;
                    mesh.Transform = translate;
                }

            }
                //Inicializamos el logo de TGC
                {
                    sceneLogoTGC = loader.loadSceneFromFile(
                     this.MediaDir + "ModelosTgc\\LogoTGC\\LogoTGC-TgcScene.xml");
                    logo = sceneLogoTGC.Meshes[0];
                    logo.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(-300, 182, 110));
                    angleY = (float)Math.PI;
                    rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                    logo.Transform = rotation * translate;




                }
                //Inicializamos el cesped de la plaza
                {
                    scenePasto = loader.loadSceneFromFile(
                      this.MediaDir + "MeshCreator\\Meshes\\Vegetacion\\Planta\\Planta-TgcScene.xml");
                    pasto = scenePasto.Meshes[0];

                    var rows = 33;
                    var cols = 36;
                    float offset = 15;
                    cesped = new List<TgcMesh>();
                    for (var i = 0; i < rows; i++)
                    {
                        for (var j = 0; j < cols; j++)
                        {
                            if (i == 2 && j == 7 || i == 28 && j == 3 || i == 15 && j == 15 || i == 20 && j == 7 || i == 3 && j == 15 || i == 17 && j == 29 || i == 23 && j == 1 || i == 1 && j == 2 || i == 21 && j == 32 || i == 30 && j == 30 || i == 6 && j == 6 || i == 22 && j == 28 || i == 5 && j == 31 || i == 12 && j == 27 || i == 2 && j == 17 || i == 12 && j == 14 || i == 14 && j == 34)
                            {
                                //Crear instancia de modelo
                                var instancePasto = pasto.createMeshInstance(pasto.Name + i + "_" + j);
                                //No recomendamos utilizar AutoTransform, en juegos complejos se pierde el control. mejor utilizar Transformaciones con matrices.
                                instancePasto.AutoTransformEnable = false;
                                //Desplazarlo
                                translate = Matrix.Translation(new Vector3(-450 + i * offset, 0, -525 + j * offset));
                                scale = Matrix.Scaling(new Vector3(0.5f, 0.5f, 0.5f));
                                instancePasto.Transform = scale * translate;
                                cesped.Add(instancePasto);
                            }
                        }
                    }
                }
                //Inicializamos los árboles
                //Intentamos hacer los movimientos y escalados con matrices pero no pudimos, les adjuntamos el codigo desarrollado con matrices:
                {

                    sceneArbol = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Meshes\\Vegetacion\\ArbolBananas\\ArbolBananas-TgcScene.xml");
                    arbol = sceneArbol.Meshes[0];

                    /*
                     * Para implementar esta parte decidimos crear tres listas; una con instancias de arboles, otra con cuatro instancias de pasto por arbol y otra con las posiciones de los arboles. 
                     */
                    arboles = new List<TgcMesh>();
                    pastos = new List<TgcMesh>();
                    posiciones = new List<Vector3>();
                    //Agregamos las posiciones de los arboles a la lista
                    posiciones.Add(new Vector3(0, 0, 200));
                    posiciones.Add(new Vector3(400, 0, -200));
                    posiciones.Add(new Vector3(400, 0, -130));
                    posiciones.Add(new Vector3(-500, 0, 400));
                    posiciones.Add(new Vector3(300, 0, 500));
                    posiciones.Add(new Vector3(-550, 0, -100));
                    posiciones.Add(new Vector3(100, 0, -350));
                    posiciones.Add(new Vector3(-800, 0, 750));
                    posiciones.Add(new Vector3(800, 0, -750));
                    posiciones.Add(new Vector3(450, 0, 100));

                    scale = Matrix.Scaling(new Vector3(0.5f, 0.5f, 0.5f));
                    for (var i = 0; i < 10; i++)
                    {
                        translate = Matrix.Translation(posiciones[i]);
                        var instanceArbol = arbol.createMeshInstance("arbol" + i);
                        instanceArbol.AutoTransformEnable = false;
                        instanceArbol.Transform = translate;
                        arboles.Add(instanceArbol);
                        for (var j = 0; j < 4; j++)
                        {
                            var instancePasto = pasto.createMeshInstance("pasto" + i + j);
                            instancePasto.AutoTransformEnable = false;
                            if (j == 0) translateAux = Matrix.Translation(posiciones[i] + new Vector3(7, 0, 0));
                            if (j == 1) translateAux = Matrix.Translation(posiciones[i] + new Vector3(-7, 0, 0));
                            if (j == 2) translateAux = Matrix.Translation(posiciones[i] + new Vector3(0, 0, 7));
                            if (j == 3) translateAux = Matrix.Translation(posiciones[i] + new Vector3(0, 0, -7));
                            instancePasto.Transform = scale * translateAux;
                            pastos.Add(instancePasto);
                        }
                    }


                }
                //Inicializamos la fuente
                {
                    sceneFuenteAgua = loader.loadSceneFromFile(
                      this.MediaDir + "MeshCreator\\Meshes\\Objetos\\FuenteAgua\\fuenteAgua-TgcScene.xml");
                    fuenteAgua0 = sceneFuenteAgua.Meshes[0];
                    fuenteAgua0.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(-210, 35, -265));
                    scale = Matrix.Scaling(new Vector3(1.2f, 1.2f, 1.2f));
                    fuenteAgua0.Transform = scale * translate;
                }
            {
                sceneFuenteAgua = loader.loadSceneFromFile(
                  this.MediaDir + "MeshCreator\\Meshes\\Objetos\\FuenteAgua\\fuenteAgua-TgcScene.xml");
                fuenteAgua1 = sceneFuenteAgua.Meshes[0];
                    fuenteAgua1.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(-210, 35, 1735));
                    scale = Matrix.Scaling(new Vector3(1.2f, 1.2f, 1.2f));
                    fuenteAgua1.Transform = scale * translate;
                }
            {
                sceneFuenteAgua = loader.loadSceneFromFile(
                  this.MediaDir + "MeshCreator\\Meshes\\Objetos\\FuenteAgua\\fuenteAgua-TgcScene.xml");
                fuenteAgua2 = sceneFuenteAgua.Meshes[0];
                    fuenteAgua2.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(-2210, 35, 1735));
                    scale = Matrix.Scaling(new Vector3(1.2f, 1.2f, 1.2f));
                    fuenteAgua2.Transform = scale * translate;
                }
            {
                sceneFuenteAgua = loader.loadSceneFromFile(
                  this.MediaDir + "MeshCreator\\Meshes\\Objetos\\FuenteAgua\\fuenteAgua-TgcScene.xml");
                fuenteAgua3 = sceneFuenteAgua.Meshes[0];
                    fuenteAgua3.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(-2210, 35, -265));
                    scale = Matrix.Scaling(new Vector3(1.2f, 1.2f, 1.2f));
                    fuenteAgua3.Transform = scale * translate;
                }
            //Inicializamos las escaleras
            {
                    sceneEscalera = loader.loadSceneFromFile(
                      this.MediaDir + "MeshCreator\\Meshes\\Muebles\\EscaleraMetalMovil\\EscaleraMetalMovil-TgcScene.xml");
                    escalera = sceneEscalera.Meshes[0];
                    var posicionInicial = new Vector3(-557, 3, -212);
                    angleY = (float)Math.PI / 2;
                    rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                    scale = Matrix.Scaling(new Vector3(0.7f, 0.7f, 0.7f));

                    escaleras = new List<TgcMesh>();

                    for (var i = 0; i < 9; i++)
                    {
                        var instanceEscalera = escalera.createMeshInstance("escalera" + i);
                        instanceEscalera.AutoTransformEnable = false;
                        if (i != 8) translate = Matrix.Translation(posicionInicial + new Vector3(0, i * 49, 0)); //offset entre escaleras
                        else translate = Matrix.Translation(new Vector3(-557, 366, -212)); //La ultima escalera tiene una posicion especial para que no sobresalga demasiado del edificio
                        instanceEscalera.Transform = scale * rotation * translate;
                        instanceEscalera.setColor(Color.Black); //Seteamos el color negro
                        escaleras.Add(instanceEscalera);

                    }



                }
                //Inicializamos metralladora militar
                {

                    sceneMetraMilitar = loader.loadSceneFromFile(
                      this.MediaDir + "MeshCreator\\Meshes\\Armas\\MetralladoraFija2\\MetralladoraFija2-TgcScene.xml");
                    metraMilitar = sceneMetraMilitar.Meshes[0];
                    metraMilitar.AutoTransformEnable = false;
                    scale = Matrix.Scaling(new Vector3(0.8f, 0.8f, 0.8f));
                    angleY = (float)Math.PI * 3 / 2;
                    rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                    translate = Matrix.Translation(new Vector3(-570, 417, -315));
                    metraMilitar.Transform = scale * rotation * translate;
                }
                //Inicializamos la camioneta
                {
                    sceneCamioneta = loader.loadSceneFromFile(this.MediaDir + "MeshCreator\\Meshes\\Vehiculos\\Camioneta\\Camioneta-TgcScene.xml");
                    camioneta = sceneCamioneta.Meshes[0];
                    camioneta.AutoTransformEnable = false;
                    scale = Matrix.Scaling(new Vector3(0.7f, 0.7f, 0.7f));
                    angleY = (float)Math.PI / 4;
                    rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                    translate = Matrix.Translation(new Vector3(-85, 5, -830));
                    camioneta.Transform = scale * rotation * translate;
                }
                //Inicializamos patrulleros
                {
                    scenePatrullero = loader.loadSceneFromFile(
                        this.MediaDir + "MeshCreator\\Meshes\\Vehiculos\\Patrullero\\Patrullero-TgcScene.xml");
                    patrullero = scenePatrullero.Meshes[0];

                    posPatrulleros = new List<Vector3>();

                    posPatrulleros.Add(new Vector3(947, 5, 600));
                    posPatrulleros.Add(new Vector3(947, 5, 0));
                    posPatrulleros.Add(new Vector3(947, 5, -500));
                    posPatrulleros.Add(new Vector3(142, 5, 105));

                    patrulleros = new List<TgcMesh>();

                    angleY = (float)Math.PI * 3 / 4;
                    rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);

                    for (var i = 0; i < 4; i++)
                    {
                        var instancePatrullero = patrullero.createMeshInstance("patrullero" + i);
                        instancePatrullero.AutoTransformEnable = false;
                        translate = Matrix.Translation(posPatrulleros[i]);
                        if (i == 3) instancePatrullero.Transform = rotation * translate;
                        else instancePatrullero.Transform = translate;
                        patrulleros.Add(instancePatrullero);
                    }
                }
                //Inicializamos maquinas
                {
                    sceneMaquina = loader.loadSceneFromFile(
                       this.MediaDir + "MeshCreator\\Meshes\\Muebles\\ExpendedorDeBebidas\\ExpendedorDeBebidas-TgcScene.xml");
                    maquina = sceneMaquina.Meshes[0];
                    maquina.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(352, 5, 787));
                    angleY = (float)Math.PI;
                    rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                    scale = Matrix.Scaling(new Vector3(0.5f, 0.5f, 0.5f));
                    maquina.Transform = scale * rotation * translate;

                    maquinas = new List<TgcMesh>();

                    var instanceMaquina1 = maquina.createMeshInstance("maquina1");
                    instanceMaquina1.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(679, 5, 787));
                    instanceMaquina1.Transform = scale * rotation * translate;
                    maquinas.Add(instanceMaquina1);
                }
                //Inicializamos cajas de munición
                {
                    sceneCajaMunicion = loader.loadSceneFromFile(this.MediaDir + "MeshCreator\\Meshes\\Armas\\CajaMuniciones\\CajaMuniciones-TgcScene.xml");
                    cajaMunicion = sceneCajaMunicion.Meshes[0];
                    cajaMunicion.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(25, 4, 380));
                    scale = Matrix.Scaling(new Vector3(0.75f, 0.75f, 0.75f));
                    cajaMunicion.Transform = scale * translate;

                    CajaMuniciones = new List<TgcMesh>();

                    var instanceCajaMunicion = cajaMunicion.createMeshInstance("caja1");
                    instanceCajaMunicion.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(-200, 4, -700));
                    instanceCajaMunicion.Transform = scale * translate;
                    CajaMuniciones.Add(instanceCajaMunicion);
                }
                //Inicializamos barriles
                {
                    sceneBarril = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Meshes\\Objetos\\BarrilPolvora\\BarrilPolvora-TgcScene.xml");
                    barril = sceneBarril.Meshes[0];
                    posBarriles = new List<Vector3>();

                    // Agregamos las posiciones de los barriles a la lista de posiciones
                    posBarriles.Add(new Vector3(-233, 5, 73));
                    posBarriles.Add(new Vector3(-535, 5, -250));
                    posBarriles.Add(new Vector3(-223, 5, -620));
                    posBarriles.Add(new Vector3(126, 5, -256));
                    posBarriles.Add(new Vector3(219, 5, -424));
                    posBarriles.Add(new Vector3(494, 5, -573));
                    posBarriles.Add(new Vector3(543, 5, -351));
                    posBarriles.Add(new Vector3(543, 5, -40));
                    posBarriles.Add(new Vector3(145, 46, 88));
                    posBarriles.Add(new Vector3(141, 5, 458));
                    posBarriles.Add(new Vector3(243, 5, 821));
                    posBarriles.Add(new Vector3(543, 5, -351));
                    posBarriles.Add(new Vector3(-808, 5, -745));
                    posBarriles.Add(new Vector3(-815, 5, -456));
                    posBarriles.Add(new Vector3(-658, 5, -386));
                    posBarriles.Add(new Vector3(-663, 5, -118));
                    posBarriles.Add(new Vector3(-863, 5, 120));
                    posBarriles.Add(new Vector3(-478, 5, 256));
                    posBarriles.Add(new Vector3(-440, 5, 534));
                    posBarriles.Add(new Vector3(-812, 5, -482));
                    posBarriles.Add(new Vector3(-277, 5, 559));
                    posBarriles.Add(new Vector3(-120, 5, 242));
                    posBarriles.Add(new Vector3(530, 5, 248));
                    posBarriles.Add(new Vector3(-468, 5, -835));
                    posBarriles.Add(new Vector3(122, 5, -814));
                    posBarriles.Add(new Vector3(658, 5, -818));
                    posBarriles.Add(new Vector3(817, 5, -637));
                    posBarriles.Add(new Vector3(809, 5, -197));
                    posBarriles.Add(new Vector3(819, 5, 528));
                    posBarriles.Add(new Vector3(758, 5, 808));
                    


                    barriles = new List<TgcMesh>();
                    scale = Matrix.Scaling(0.5f, 0.5f, 0.5f);


                    for (var i = 0; i < 28; i++)
                    {   // Por cada instancia de barril que creamos, le asignamos una posicion y lo añadimos a la lista de barriles
                        var instanceBarril = barril.createMeshInstance("barril" + i);
                        instanceBarril.AutoTransformEnable = false;
                        translate = Matrix.Translation(posBarriles[i]);
                        instanceBarril.Transform = scale * translate;
                        barriles.Add(instanceBarril);
                    }
                    for(var i=0; i < 28; i++) { posBarriles[i] += new Vector3(0, 0, 2000); }
                for (var i = 0; i < 28; i++)
                {   // Por cada instancia de barril que creamos, le asignamos una posicion y lo añadimos a la lista de barriles
                    var j = i + 28;
                    var instanceBarril = barril.createMeshInstance("barril" + j);
                    instanceBarril.AutoTransformEnable = false;
                    translate = Matrix.Translation(posBarriles[i]);
                    instanceBarril.Transform = scale * translate;
                    barriles.Add(instanceBarril);
                }
                for (var i = 0; i < 28; i++) { posBarriles[i] += new Vector3(-2000, 0, 0); }
                for (var i = 0; i < 28; i++)
                {   // Por cada instancia de barril que creamos, le asignamos una posicion y lo añadimos a la lista de barriles
                    var j = i + 56;
                    var instanceBarril = barril.createMeshInstance("barril" + j);
                    instanceBarril.AutoTransformEnable = false;
                    translate = Matrix.Translation(posBarriles[i]);
                    instanceBarril.Transform = scale * translate;
                    barriles.Add(instanceBarril);
                }
                for (var i = 0; i < 28; i++) { posBarriles[i] += new Vector3(0, 0, -2000); }
                for (var i = 0; i < 28; i++)
                {   // Por cada instancia de barril que creamos, le asignamos una posicion y lo añadimos a la lista de barriles
                    var j = i + 84;
                    var instanceBarril = barril.createMeshInstance("barril" + j);
                    instanceBarril.AutoTransformEnable = false;
                    translate = Matrix.Translation(posBarriles[i]);
                    instanceBarril.Transform = scale * translate;
                    barriles.Add(instanceBarril);
                }
            }


                //Inicializamos metralladoras fijas
                {
                    sceneMetralladoraFija = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Meshes\\Armas\\MetralladoraFija\\metralladoraFija-TgcScene.xml");
                    metralladoraFija = sceneMetralladoraFija.Meshes[0];
                    metralladoraFija.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(0, 5, 385));
                    scale = Matrix.Scaling(new Vector3(0.75f, 0.75f, 0.75f));
                    metralladoraFija.Transform = scale * translate;

                    metralladoras = new List<TgcMesh>();

                    var instanceMetra1 = metralladoraFija.createMeshInstance("metra1");
                    instanceMetra1.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(-175, 5, -700));
                    scale = Matrix.Scaling(new Vector3(0.75f, 0.75f, 0.75f));
                    angleY = (float)Math.PI;
                    rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                    instanceMetra1.Transform = scale * rotation * translate;
                    metralladoras.Add(instanceMetra1);

                }
                //Inicializamos las paredes de la plaza       
                {
                    scenePared = loader.loadSceneFromFile(
                        this.MediaDir + "ModelosTgc\\Pared\\ParedBlanca-TgcScene.xml");
                    pared = scenePared.Meshes[0];
                    pared.Scale = new Vector3(0.2f, 0.2f, 1.6f);
                    angleY = (float)Math.PI / 2;
                    paredes = new List<TgcMesh>();
                    posParedes = new List<Vector3>();

                    posParedes.Add(new Vector3(40, 0, -105));
                    posParedes.Add(new Vector3(40, 0, -418));
                    posParedes.Add(new Vector3(-460, 0, -418));
                    posParedes.Add(new Vector3(-460, 0, -105));

                    posParedes.Add(new Vector3(-47, 0, -540));
                    posParedes.Add(new Vector3(-373, 0, -540));
                    posParedes.Add(new Vector3(-373, 0, 17));
                    posParedes.Add(new Vector3(-47, 0, 17));

                    posParedes.Add(new Vector3(40, 0, 1895));
                    posParedes.Add(new Vector3(40, 0, 1582));
                    posParedes.Add(new Vector3(-460, 0, 1582));
                    posParedes.Add(new Vector3(-460, 0, 1895));

                    posParedes.Add(new Vector3(-47, 0, 1460));
                    posParedes.Add(new Vector3(-373, 0, 1460));
                    posParedes.Add(new Vector3(-373, 0, 2017));
                    posParedes.Add(new Vector3(-47, 0, 2017));

                    posParedes.Add(new Vector3(-1960, 0, 1895));
                    posParedes.Add(new Vector3(-1960, 0, 1582));
                    posParedes.Add(new Vector3(-2460, 0, 1582));
                    posParedes.Add(new Vector3(-2460, 0, 1895));

                    posParedes.Add(new Vector3(-2047, 0, 1460));
                    posParedes.Add(new Vector3(-2373, 0, 1460));
                    posParedes.Add(new Vector3(-2373, 0, 2017));
                    posParedes.Add(new Vector3(-2047, 0, 2017));

                    posParedes.Add(new Vector3(-1960, 0, -105));
                    posParedes.Add(new Vector3(-1960, 0, -418));
                    posParedes.Add(new Vector3(-2460, 0, -418));
                    posParedes.Add(new Vector3(-2460, 0, -105));

                    posParedes.Add(new Vector3(-2047, 0, -540));
                    posParedes.Add(new Vector3(-2373, 0, -540));
                    posParedes.Add(new Vector3(-2373, 0, 17));
                    posParedes.Add(new Vector3(-2047, 0, 17));

                for (var i = 0; i < 32; i++)
                    {// Por cada instancia de pared que creamos, le asignamos una posicion, y depende de la orientacion lo escalamos y/o rotamos de distintas formas para luegO añadirlo a la lista de paredes
                        var instancePared = pared.createMeshInstance("pared" + i);
                        instancePared.AutoTransformEnable = false;
                        translate = Matrix.Translation(posParedes[i]);
                        if (i < 4)
                        {
                            scale = Matrix.Scaling(new Vector3(0.2f, 0.2f, 1.6f));
                            instancePared.Transform = scale * translate;
                        }
                        else if (i<8)
                        {
                            scale = Matrix.Scaling(new Vector3(0.2f, 0.2f, 1.2f));
                            rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                            instancePared.Transform = scale * rotation * translate;
                        }
                        else if (i < 12)
                        {
                            scale = Matrix.Scaling(new Vector3(0.2f, 0.2f, 1.6f));
                            instancePared.Transform = scale * translate;
                        }
                        else if (i < 16)
                        {
                            scale = Matrix.Scaling(new Vector3(0.2f, 0.2f, 1.2f));
                            rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                            instancePared.Transform = scale * rotation * translate;
                        }
                        else if (i < 20)
                        {
                            scale = Matrix.Scaling(new Vector3(0.2f, 0.2f, 1.6f));
                            instancePared.Transform = scale * translate;
                        }
                        else if (i < 24)
                        {
                            scale = Matrix.Scaling(new Vector3(0.2f, 0.2f, 1.2f));
                            rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                            instancePared.Transform = scale * rotation * translate;
                        }
                        else if (i < 28)
                        {
                            scale = Matrix.Scaling(new Vector3(0.2f, 0.2f, 1.6f));
                            instancePared.Transform = scale * translate;
                        }
                        else if (i < 32)
                        {
                            scale = Matrix.Scaling(new Vector3(0.2f, 0.2f, 1.2f));
                            rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                            instancePared.Transform = scale * rotation * translate;
                        }


                    paredes.Add(instancePared);
                    }



                }
                //helicoptero
                {
                    sceneHelicoptero = loader.loadSceneFromFile(
                      this.MediaDir + "MeshCreator\\Meshes\\Vehiculos\\HelicopteroMilitar\\HelicopteroMilitar-TgcScene.xml");
                    helicoptero = sceneHelicoptero.Meshes[0];
                    helicoptero.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(-900, 5, -110));
                    angleY = (float)Math.PI * 3 / 2;
                    rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                    helicoptero.Transform = rotation * translate;


                }
                //Inicializamos aviones
                {
                    sceneAvion = loader.loadSceneFromFile(
                          this.MediaDir + "MeshCreator\\Meshes\\Vehiculos\\AvionMilitar\\AvionMilitar-TgcScene.xml");
                    avion = sceneAvion.Meshes[0];
                    avion.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(-362, 1000, -869));
                    angleY = (float)Math.PI;
                    rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                    avion.Transform = rotation * translate;

                    aviones = new List<TgcMesh>();
                    var instanceAvion1 = avion.createMeshInstance("avion1");
                    instanceAvion1.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(308, 1000, 605));
                    instanceAvion1.Transform = translate;
                    aviones.Add(instanceAvion1);
                }
                //Inicializamos Hammer
                {
                    sceneHammer = loader.loadSceneFromFile(
                   this.MediaDir + "MeshCreator\\Meshes\\Vehiculos\\Hummer\\Hummer-TgcScene.xml");
                    hammer = sceneHammer.Meshes[0];
                    hammer.AutoTransformEnable = false;
                    translate = Matrix.Translation(new Vector3(278, 5, -540));
                    angleY = (float)Math.PI / 4;
                    rotation = Matrix.RotationYawPitchRoll(angleY, 0, 0);
                    hammer.Transform = rotation * translate;
                }
                //Device de DirectX para crear primitivas.
                {
                    var d3dDevice = D3DDevice.Instance.Device;

                    //Camara en 1ra persona
                    Camara = new TgcFpsCamera(new Vector3(-1000, 4000, 1000), 5000, 5000, Input);
                }
            }

        /// <summary>
        ///     Se llama en cada frame.
        ///     Se debe escribir toda la lógica de computo del modelo, así como también verificar entradas del usuario y reacciones
        ///     ante ellas.
        /// </summary>
        public override void Update()
        {
            PreUpdate();

        }

        /// <summary>
        ///     Se llama cada vez que hay que refrescar la pantalla.
        ///     Escribir aquí todo el código referido al renderizado.
        ///     Borrar todo lo que no haga falta.
        /// </summary>
        public override void Render()
        {
            //Inicio el render de la escena, para ejemplos simples. Cuando tenemos postprocesado o shaders es mejor realizar las operaciones según nuestra conveniencia.
            PreRender();
            //Renderizar instancias
            foreach (var mesh in cesped)
            {
                mesh.render();
            }
            DrawText.drawText(Camara.Position.ToString(), 10, 20, Color.Red);
            //Renderizar instancias
            foreach (var mesh in metralladoras)
            {
                mesh.render();
            }
            foreach (var mesh in escaleras)
            {
                mesh.render();
            }
            foreach (var mesh in paredes)
            {
                mesh.render();
            }
            foreach (var mesh in arboles)
            {
                mesh.render();
            }
            foreach (var mesh in pastos)
            {
                mesh.render();
            }
            foreach (var mesh in CajaMuniciones)
            {
                mesh.render();
            }
            foreach (var mesh in barriles)
            {
                mesh.render();

            }
            foreach (var mesh in patrulleros)
            {
                mesh.render();

            }
            foreach (var mesh in aviones)
            {
                mesh.render();
            }
            foreach (var mesh in maquinas)
            {
                mesh.render();
            }
            fuenteAgua0.render();
            fuenteAgua1.render();
            fuenteAgua2.render();
            fuenteAgua3.render();
            metralladoraFija.render();
            cajaMunicion.render();
            camioneta.render();
            helicoptero.render();
            avion.render();
            hammer.render();
            logo.render();
            maquina.render();
            metraMilitar.render();
            scene0.renderAll();
            scene1.renderAll();
            scene2.renderAll();
            scene3.renderAll();
            //Finaliza el render y presenta en pantalla, al igual que el preRender se debe para casos puntuales es mejor utilizar a mano las operaciones de EndScene y PresentScene
            PostRender();
        }

        /// <summary>
        ///     Se llama cuando termina la ejecución del ejemplo.
        ///     Hacer Dispose() de todos los objetos creados.
        ///     Es muy importante liberar los recursos, sobretodo los gráficos ya que quedan bloqueados en el device de video.
        /// </summary>
        public override void Dispose()
        {
            /*foreach (var barrio in ciudad)
            {
                barrio.disposeAll();
            }*/
            scene0.disposeAll();
            scene1.disposeAll();
            scene2.disposeAll();
            scene3.disposeAll();
            pasto.dispose();
            fuenteAgua0.dispose();
            fuenteAgua1.dispose();
            fuenteAgua2.dispose();
            fuenteAgua3.dispose();
            metralladoraFija.dispose();
            pared.dispose();
            cajaMunicion.dispose();
            camioneta.dispose();
            patrullero.dispose();
            barril.dispose();
            helicoptero.dispose();
            avion.dispose();
            hammer.dispose();
            logo.dispose();
            arbol.dispose();
            maquina.dispose();
            escalera.dispose();
            metraMilitar.dispose();
        }
    }
}