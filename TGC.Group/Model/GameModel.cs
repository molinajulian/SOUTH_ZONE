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
        private TgcScene scene,sceneArbol,sceneMetralladoraFija,scenePasto,sceneFuenteAgua,scenePared,sceneCajaMunicion,sceneCamioneta,scenePatrullero,sceneBarril,sceneHelicoptero,sceneAvion,sceneHammer,sceneLogoTGC;
        private TgcMesh pasto,arbol,fuenteAgua,metralladoraFija,pared,cajaMunicion,camioneta,patrullero,barril,helicoptero,avion,hammer,logo;
        private List<TgcMesh> cesped,metralladoras,paredes,CajaMuniciones,patrulleros,barriles,aviones,arboles1, arboles2, arboles3, arboles4, arboles5, arboles6, arboles7, arboles8, arboles9, arboles10;
        

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
            //Cargar scene -> ciudad

            TgcSceneLoader loader = new TgcSceneLoader();
            scene = loader.loadSceneFromFile(
                this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");

            scene.Meshes[0].Scale = new Vector3(1f, 2f, 1f);
            scene.Meshes[1].Scale = new Vector3(1f, 2f, 1f);


            //LogoTGC
            {
                sceneLogoTGC= loader.loadSceneFromFile(
                 this.MediaDir + "ModelosTgc\\LogoTGC\\LogoTGC-TgcScene.xml");
                logo= sceneLogoTGC.Meshes[0];
                logo.Position = new Vector3(-300, 182, 110);
                logo.rotateY((float)Math.PI);

            }
            //Cesped
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
                        //Crear instancia de modelo
                        var instancePasto = pasto.createMeshInstance(pasto.Name + i + "_" + j);
                        //No recomendamos utilizar AutoTransform, en juegos complejos se pierde el control. mejor utilizar Transformaciones con matrices.
                        instancePasto.AutoTransformEnable = true;
                        //Desplazarlo
                        instancePasto.move(-450 + i * offset, 0, -525 + j * offset);
                        instancePasto.Scale = new Vector3(0.5f, 0.5f, 0.5f);

                        cesped.Add(instancePasto);
                    }
                }
            }
            //arboles
            {
                

                sceneArbol = loader.loadSceneFromFile(
                this.MediaDir + "MeshCreator\\Meshes\\Vegetacion\\ArbolBananas\\ArbolBananas-TgcScene.xml");
                arbol = sceneArbol.Meshes[0];

                arboles1 = new List<TgcMesh>();
                arboles2 = new List<TgcMesh>();
                arboles3 = new List<TgcMesh>();
                arboles4 = new List<TgcMesh>();
                arboles5 = new List<TgcMesh>();
                arboles6 = new List<TgcMesh>();
                arboles7 = new List<TgcMesh>();
                arboles8 = new List<TgcMesh>();
                arboles9 = new List<TgcMesh>();
                arboles10 = new List<TgcMesh>();

                var instanceArbol1 = arbol.createMeshInstance("arbol1");
                var instancePasto11 = pasto.createMeshInstance("pasto11");
                var instancePasto12 = pasto.createMeshInstance("pasto12");
                var instancePasto13 = pasto.createMeshInstance("pasto13");
                var instancePasto14 = pasto.createMeshInstance("pasto14");
                instanceArbol1.AutoTransformEnable = true;
                instancePasto11.AutoTransformEnable = true;
                instancePasto12.AutoTransformEnable = true;
                instancePasto13.AutoTransformEnable = true;
                instancePasto14.AutoTransformEnable = true;
                instancePasto11.Position = new Vector3(7, 0, 0);
                instancePasto12.Position = new Vector3(-7, 0, 0);
                instancePasto13.Position = new Vector3(0, 0, 7);
                instancePasto14.Position = new Vector3(0, 0, -7);
                instancePasto11.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto12.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto13.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto14.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                arboles1.Add(instanceArbol1);
                arboles1.Add(instancePasto11);
                arboles1.Add(instancePasto12);
                arboles1.Add(instancePasto13);
                arboles1.Add(instancePasto14);
                foreach (var instance in arboles1)
                {
                    instance.move(new Vector3(0, 0, 200));
                }

                var instanceArbol2 = arbol.createMeshInstance("arbol2");
                var instancePasto21 = pasto.createMeshInstance("pasto21");
                var instancePasto22 = pasto.createMeshInstance("pasto22");
                var instancePasto23 = pasto.createMeshInstance("pasto23");
                var instancePasto24 = pasto.createMeshInstance("pasto24");
                instanceArbol2.AutoTransformEnable = true;
                instancePasto21.AutoTransformEnable = true;
                instancePasto22.AutoTransformEnable = true;
                instancePasto23.AutoTransformEnable = true;
                instancePasto24.AutoTransformEnable = true;
                instancePasto21.Position = new Vector3(7, 0, 0);
                instancePasto22.Position = new Vector3(-7, 0, 0);
                instancePasto23.Position = new Vector3(0, 0, 7);
                instancePasto24.Position = new Vector3(0, 0, -7);
                instancePasto21.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto22.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto23.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto24.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                arboles2.Add(instanceArbol2);
                arboles2.Add(instancePasto21);
                arboles2.Add(instancePasto22);
                arboles2.Add(instancePasto23);
                arboles2.Add(instancePasto24);
                foreach (var instance in arboles2)
                {
                    instance.move(new Vector3(400, 0, -200));
                }

                var instanceArbol3 = arbol.createMeshInstance("arbol3");
                var instancePasto31 = pasto.createMeshInstance("pasto31");
                var instancePasto32 = pasto.createMeshInstance("pasto32");
                var instancePasto33 = pasto.createMeshInstance("pasto33");
                var instancePasto34 = pasto.createMeshInstance("pasto34");
                instanceArbol3.AutoTransformEnable = true;
                instancePasto31.AutoTransformEnable = true;
                instancePasto32.AutoTransformEnable = true;
                instancePasto33.AutoTransformEnable = true;
                instancePasto34.AutoTransformEnable = true;
                instancePasto31.Position = new Vector3(7, 0, 0);
                instancePasto32.Position = new Vector3(-7, 0, 0);
                instancePasto33.Position = new Vector3(0, 0, 7);
                instancePasto34.Position = new Vector3(0, 0, -7);
                instancePasto31.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto32.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto33.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto34.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                arboles3.Add(instanceArbol3);
                arboles3.Add(instancePasto31);
                arboles3.Add(instancePasto32);
                arboles3.Add(instancePasto33);
                arboles3.Add(instancePasto34);
                foreach (var instance in arboles3)
                {
                    instance.move(new Vector3(400, 0, -130));
                }

                var instanceArbol4 = arbol.createMeshInstance("arbol4");
                var instancePasto41 = pasto.createMeshInstance("pasto41");
                var instancePasto42 = pasto.createMeshInstance("pasto42");
                var instancePasto43 = pasto.createMeshInstance("pasto43");
                var instancePasto44 = pasto.createMeshInstance("pasto44");
                instanceArbol4.AutoTransformEnable = true;
                instancePasto41.AutoTransformEnable = true;
                instancePasto42.AutoTransformEnable = true;
                instancePasto43.AutoTransformEnable = true;
                instancePasto44.AutoTransformEnable = true;
                instancePasto41.Position = new Vector3(7, 0, 0);
                instancePasto42.Position = new Vector3(-7, 0, 0);
                instancePasto43.Position = new Vector3(0, 0, 7);
                instancePasto44.Position = new Vector3(0, 0, -7);
                instancePasto41.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto42.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto43.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto44.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                arboles4.Add(instanceArbol4);
                arboles4.Add(instancePasto41);
                arboles4.Add(instancePasto42);
                arboles4.Add(instancePasto43);
                arboles4.Add(instancePasto44);
                foreach (var instance in arboles4)
                {
                    instance.move(new Vector3(-500, 0, 400));
                }

                var instanceArbol5 = arbol.createMeshInstance("arbol5");
                var instancePasto51 = pasto.createMeshInstance("pasto51");
                var instancePasto52 = pasto.createMeshInstance("pasto52");
                var instancePasto53 = pasto.createMeshInstance("pasto53");
                var instancePasto54 = pasto.createMeshInstance("pasto54");
                instanceArbol5.AutoTransformEnable = true;
                instancePasto51.AutoTransformEnable = true;
                instancePasto52.AutoTransformEnable = true;
                instancePasto53.AutoTransformEnable = true;
                instancePasto54.AutoTransformEnable = true;
                instancePasto51.Position = new Vector3(7, 0, 0);
                instancePasto52.Position = new Vector3(-7, 0, 0);
                instancePasto53.Position = new Vector3(0, 0, 7);
                instancePasto54.Position = new Vector3(0, 0, -7);
                instancePasto51.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto52.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto53.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto54.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                arboles5.Add(instanceArbol5);
                arboles5.Add(instancePasto51);
                arboles5.Add(instancePasto52);
                arboles5.Add(instancePasto53);
                arboles5.Add(instancePasto54);
                foreach (var instance in arboles5)
                {
                    instance.move(new Vector3(300, 0, 500));
                }

                var instanceArbol6 = arbol.createMeshInstance("arbol6");
                var instancePasto61 = pasto.createMeshInstance("pasto61");
                var instancePasto62 = pasto.createMeshInstance("pasto62");
                var instancePasto63 = pasto.createMeshInstance("pasto63");
                var instancePasto64 = pasto.createMeshInstance("pasto64");
                instanceArbol6.AutoTransformEnable = true;
                instancePasto61.AutoTransformEnable = true;
                instancePasto62.AutoTransformEnable = true;
                instancePasto63.AutoTransformEnable = true;
                instancePasto64.AutoTransformEnable = true;
                instancePasto61.Position = new Vector3(7, 0, 0);
                instancePasto62.Position = new Vector3(-7, 0, 0);
                instancePasto63.Position = new Vector3(0, 0, 7);
                instancePasto64.Position = new Vector3(0, 0, -7);
                instancePasto61.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto62.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto63.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto64.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                arboles6.Add(instanceArbol6);
                arboles6.Add(instancePasto61);
                arboles6.Add(instancePasto62);
                arboles6.Add(instancePasto63);
                arboles6.Add(instancePasto64);
                foreach (var instance in arboles6)
                {
                    instance.move(new Vector3(-550, 0, -100));
                }

                var instanceArbol7 = arbol.createMeshInstance("arbol7");
                var instancePasto71 = pasto.createMeshInstance("pasto71");
                var instancePasto72 = pasto.createMeshInstance("pasto72");
                var instancePasto73 = pasto.createMeshInstance("pasto73");
                var instancePasto74 = pasto.createMeshInstance("pasto74");
                instanceArbol7.AutoTransformEnable = true;
                instancePasto71.AutoTransformEnable = true;
                instancePasto72.AutoTransformEnable = true;
                instancePasto73.AutoTransformEnable = true;
                instancePasto74.AutoTransformEnable = true;
                instancePasto71.Position = new Vector3(7, 0, 0);
                instancePasto72.Position = new Vector3(-7, 0, 0);
                instancePasto73.Position = new Vector3(0, 0, 7);
                instancePasto74.Position = new Vector3(0, 0, -7);
                instancePasto71.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto72.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto73.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto74.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                arboles7.Add(instanceArbol7);
                arboles7.Add(instancePasto71);
                arboles7.Add(instancePasto72);
                arboles7.Add(instancePasto73);
                arboles7.Add(instancePasto74);
                foreach (var instance in arboles7)
                {
                    instance.move(new Vector3(100, 0, -350));
                }

                var instanceArbol8 = arbol.createMeshInstance("arbol8");
                var instancePasto81 = pasto.createMeshInstance("pasto81");
                var instancePasto82 = pasto.createMeshInstance("pasto82");
                var instancePasto83 = pasto.createMeshInstance("pasto83");
                var instancePasto84 = pasto.createMeshInstance("pasto84");
                instanceArbol8.AutoTransformEnable = true;
                instancePasto81.AutoTransformEnable = true;
                instancePasto82.AutoTransformEnable = true;
                instancePasto83.AutoTransformEnable = true;
                instancePasto84.AutoTransformEnable = true;
                instancePasto81.Position = new Vector3(7, 0, 0);
                instancePasto82.Position = new Vector3(-7, 0, 0);
                instancePasto83.Position = new Vector3(0, 0, 7);
                instancePasto84.Position = new Vector3(0, 0, -7);
                instancePasto81.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto82.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto83.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto84.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                arboles8.Add(instanceArbol8);
                arboles8.Add(instancePasto81);
                arboles8.Add(instancePasto82);
                arboles8.Add(instancePasto83);
                arboles8.Add(instancePasto84);
                foreach (var instance in arboles8)
                {
                    instance.move(new Vector3(-800, 0, 750));
                }

                var instanceArbol9 = arbol.createMeshInstance("arbol9");
                var instancePasto91 = pasto.createMeshInstance("pasto91");
                var instancePasto92 = pasto.createMeshInstance("pasto92");
                var instancePasto93 = pasto.createMeshInstance("pasto93");
                var instancePasto94 = pasto.createMeshInstance("pasto94");
                instanceArbol9.AutoTransformEnable = true;
                instancePasto91.AutoTransformEnable = true;
                instancePasto92.AutoTransformEnable = true;
                instancePasto93.AutoTransformEnable = true;
                instancePasto94.AutoTransformEnable = true;
                instancePasto91.Position = new Vector3(7, 0, 0);
                instancePasto92.Position = new Vector3(-7, 0, 0);
                instancePasto93.Position = new Vector3(0, 0, 7);
                instancePasto94.Position = new Vector3(0, 0, -7);
                instancePasto91.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto92.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto93.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto94.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                arboles9.Add(instanceArbol9);
                arboles9.Add(instancePasto91);
                arboles9.Add(instancePasto92);
                arboles9.Add(instancePasto93);
                arboles9.Add(instancePasto94);
                foreach (var instance in arboles9)
                {
                    instance.move(new Vector3(800, 0, -750));
                }

                var instanceArbol10 = arbol.createMeshInstance("arbol10");
                var instancePasto101 = pasto.createMeshInstance("pasto101");
                var instancePasto102 = pasto.createMeshInstance("pasto102");
                var instancePasto103 = pasto.createMeshInstance("pasto103");
                var instancePasto104 = pasto.createMeshInstance("pasto104");
                instanceArbol10.AutoTransformEnable = true;
                instancePasto101.AutoTransformEnable = true;
                instancePasto102.AutoTransformEnable = true;
                instancePasto103.AutoTransformEnable = true;
                instancePasto104.AutoTransformEnable = true;
                instancePasto101.Position = new Vector3(7, 0, 0);
                instancePasto102.Position = new Vector3(-7, 0, 0);
                instancePasto103.Position = new Vector3(0, 0, 7);
                instancePasto104.Position = new Vector3(0, 0, -7);
                instancePasto101.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto102.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto103.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                instancePasto104.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                arboles10.Add(instanceArbol10);
                arboles10.Add(instancePasto101);
                arboles10.Add(instancePasto102);
                arboles10.Add(instancePasto103);
                arboles10.Add(instancePasto104);
                foreach (var instance in arboles10)
                {
                    instance.move(new Vector3(450, 0, 100));
                }

            }
            //fuente
            {
                sceneFuenteAgua = loader.loadSceneFromFile(
                  this.MediaDir + "MeshCreator\\Meshes\\Objetos\\FuenteAgua\\fuenteAgua-TgcScene.xml");
                fuenteAgua = sceneFuenteAgua.Meshes[0];
                fuenteAgua.Position = new Vector3(-210, 35, -265);
                fuenteAgua.Scale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            //camioneta
            {
                sceneCamioneta = loader.loadSceneFromFile(this.MediaDir + "MeshCreator\\Meshes\\Vehiculos\\Camioneta\\Camioneta-TgcScene.xml");
                camioneta = sceneCamioneta.Meshes[0];
                camioneta.Position = new Vector3(-85, 5, -830);
                camioneta.rotateY((float)Math.PI / 4);
                camioneta.Scale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            //patrulleros
            {
                scenePatrullero = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Meshes\\Vehiculos\\Patrullero\\Patrullero-TgcScene.xml");
                patrullero = scenePatrullero.Meshes[0];
                patrullero.Position = new Vector3(947, 5, 600);
                patrulleros = new List<TgcMesh>();

                var instancePatrullero1 = patrullero.createMeshInstance("patrullero1");
                instancePatrullero1.AutoTransformEnable = true;
                instancePatrullero1.Position = new Vector3(947, 5, 0);
                patrulleros.Add(instancePatrullero1);

                var instancePatrullero2 = patrullero.createMeshInstance("patrullero2");
                instancePatrullero2.AutoTransformEnable = true;
                instancePatrullero2.Position = new Vector3(947, 5, -500);
                patrulleros.Add(instancePatrullero2);

                var instancePatrullero3 = patrullero.createMeshInstance("patrullero3");
                instancePatrullero3.AutoTransformEnable = true;
                instancePatrullero3.Position = new Vector3(142, 5, 105);
                instancePatrullero3.rotateY((float)Math.PI *3 / 4);
                patrulleros.Add(instancePatrullero3);
            } 
            // Caja de municiones
            {
                sceneCajaMunicion = loader.loadSceneFromFile(this.MediaDir + "MeshCreator\\Meshes\\Armas\\CajaMuniciones\\CajaMuniciones-TgcScene.xml");
                cajaMunicion = sceneCajaMunicion.Meshes[0];
                cajaMunicion.Position = new Vector3(25, 4, 380);
                cajaMunicion.Scale = new Vector3(0.75f, 0.75f, 0.75f);

                var instanceCaja1 = cajaMunicion.createMeshInstance("caja1");
                instanceCaja1.AutoTransformEnable = true;

                CajaMuniciones = new List<TgcMesh>();
                instanceCaja1.Position = new Vector3(-200, 4, -700);
                instanceCaja1.Scale = new Vector3(0.75f, 0.75f, 0.75f);
                CajaMuniciones.Add(instanceCaja1);
            }
            // barriles
            {
                sceneBarril = loader.loadSceneFromFile(
                this.MediaDir + "MeshCreator\\Meshes\\Objetos\\BarrilPolvora\\BarrilPolvora-TgcScene.xml");
                barril = sceneBarril.Meshes[0];
                barril.Position = new Vector3(-233, 5, 73);
                barril.Scale = new Vector3(0.5f, 0.5f, 0.5f);

                barriles = new List<TgcMesh>();
                var instanceBarril1 = barril.createMeshInstance("barril1");
                instanceBarril1.AutoTransformEnable = true;
                instanceBarril1.Position = new Vector3(-535, 5, -250);
                instanceBarril1.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril1);

                var instanceBarril2 = barril.createMeshInstance("barril2");
                instanceBarril2.AutoTransformEnable = true;
                instanceBarril2.Position = new Vector3(-223, 5, -620);
                instanceBarril2.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril2);

                var instanceBarril3 = barril.createMeshInstance("barril3");
                instanceBarril3.AutoTransformEnable = true;
                instanceBarril3.Position = new Vector3(126, 5, -256);
                instanceBarril3.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril3);

                var instanceBarril4 = barril.createMeshInstance("barril4");
                instanceBarril4.AutoTransformEnable = true;
                instanceBarril4.Position = new Vector3(219, 5, -424);
                instanceBarril4.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril4);

                var instanceBarril5 = barril.createMeshInstance("barril5");
                instanceBarril5.AutoTransformEnable = true;
                instanceBarril5.Position = new Vector3(494, 5, -573);
                instanceBarril5.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril5);

                var instanceBarril6 = barril.createMeshInstance("barril6");
                instanceBarril6.AutoTransformEnable = true;
                instanceBarril6.Position = new Vector3(543, 5, -351);
                instanceBarril6.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril6);

                var instanceBarril7 = barril.createMeshInstance("barril7");
                instanceBarril7.AutoTransformEnable = true;
                instanceBarril7.Position = new Vector3(543, 5, -40);
                instanceBarril7.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril7);

                var instanceBarril8 = barril.createMeshInstance("barril8");
                instanceBarril8.AutoTransformEnable = true;
                instanceBarril8.Position = new Vector3(145, 46, 88);
                instanceBarril8.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril8);

                var instanceBarril9 = barril.createMeshInstance("barril9");
                instanceBarril9.AutoTransformEnable = true;
                instanceBarril9.Position = new Vector3(141, 5, 458);
                instanceBarril9.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril9);

                var instanceBarril10 = barril.createMeshInstance("barril10");
                instanceBarril10.AutoTransformEnable = true;
                instanceBarril10.Position = new Vector3(243, 5, 821);
                instanceBarril10.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril10);

                var instanceBarril11 = barril.createMeshInstance("barril11");
                instanceBarril11.AutoTransformEnable = true;
                instanceBarril11.Position = new Vector3(543, 5, -351);
                instanceBarril11.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril11);

                var instanceBarril12 = barril.createMeshInstance("barril12");
                instanceBarril11.AutoTransformEnable = true;
                instanceBarril11.Position = new Vector3(-808, 5, -745);
                instanceBarril11.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril11);

                var instanceBarril13 = barril.createMeshInstance("barril13");
                instanceBarril13.AutoTransformEnable = true;
                instanceBarril13.Position = new Vector3(-815, 5, -456);
                instanceBarril13.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril13);

                var instanceBarril14 = barril.createMeshInstance("barril14");
                instanceBarril14.AutoTransformEnable = true;
                instanceBarril14.Position = new Vector3(-658, 5, -386);
                instanceBarril14.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril14);

                var instanceBarril15 = barril.createMeshInstance("barril15");
                instanceBarril15.AutoTransformEnable = true;
                instanceBarril15.Position = new Vector3(-663, 5, -118);
                instanceBarril15.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril15);

                var instanceBarril16 = barril.createMeshInstance("barril16");
                instanceBarril16.AutoTransformEnable = true;
                instanceBarril16.Position = new Vector3(-863, 5, 120);
                instanceBarril16.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril16);

                var instanceBarril17 = barril.createMeshInstance("barril17");
                instanceBarril17.AutoTransformEnable = true;
                instanceBarril17.Position = new Vector3(-478, 5, 256);
                instanceBarril17.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril17);

                var instanceBarril18 = barril.createMeshInstance("barril18");
                instanceBarril18.AutoTransformEnable = true;
                instanceBarril18.Position = new Vector3(-440, 5, 534);
                instanceBarril18.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril18);

                var instanceBarril19 = barril.createMeshInstance("barril19");
                instanceBarril19.AutoTransformEnable = true;
                instanceBarril19.Position = new Vector3(-812, 5, -482);
                instanceBarril19.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril19);

                var instanceBarril20 = barril.createMeshInstance("barril20");
                instanceBarril20.AutoTransformEnable = true;
                instanceBarril20.Position = new Vector3(-277, 5, 559);
                instanceBarril20.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril20);

                var instanceBarril21 = barril.createMeshInstance("barril21");
                instanceBarril21.AutoTransformEnable = true;
                instanceBarril21.Position = new Vector3(-120, 5, 242);
                instanceBarril21.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril21);

                var instanceBarril22 = barril.createMeshInstance("barril22");
                instanceBarril22.AutoTransformEnable = true;
                instanceBarril22.Position = new Vector3(530, 5, 248);
                instanceBarril22.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril22);

                var instanceBarril23 = barril.createMeshInstance("barril23");
                instanceBarril23.AutoTransformEnable = true;
                instanceBarril23.Position = new Vector3(-468, 5, -835);
                instanceBarril23.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril23);

                var instanceBarril24 = barril.createMeshInstance("barril24");
                instanceBarril24.AutoTransformEnable = true;
                instanceBarril24.Position = new Vector3(122, 5, -814);
                instanceBarril24.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril24);

                var instanceBarril25 = barril.createMeshInstance("barril25");
                instanceBarril25.AutoTransformEnable = true;
                instanceBarril25.Position = new Vector3(658, 5, -818);
                instanceBarril25.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril25);

                var instanceBarril26 = barril.createMeshInstance("barril126");
                instanceBarril26.AutoTransformEnable = true;
                instanceBarril26.Position = new Vector3(817, 5, -637);
                instanceBarril26.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril26);

                var instanceBarril27 = barril.createMeshInstance("barril27");
                instanceBarril27.AutoTransformEnable = true;
                instanceBarril27.Position = new Vector3(809, 5, -197);
                instanceBarril27.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril11);

                var instanceBarril28 = barril.createMeshInstance("barril28");
                instanceBarril28.AutoTransformEnable = true;
                instanceBarril28.Position = new Vector3(819, 5, 528);
                instanceBarril28.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril28);

                var instanceBarril29 = barril.createMeshInstance("barril29");
                instanceBarril29.AutoTransformEnable = true;
                instanceBarril29.Position = new Vector3(758, 5, 808);
                instanceBarril29.Scale = new Vector3(0.5f, 0.5f, 0.5f);
                barriles.Add(instanceBarril29);
            }
            //metralladoras
            {
                sceneMetralladoraFija = loader.loadSceneFromFile(
                this.MediaDir + "MeshCreator\\Meshes\\Armas\\MetralladoraFija\\metralladoraFija-TgcScene.xml");
                metralladoraFija = sceneMetralladoraFija.Meshes[0];
                metralladoraFija.Position = new Vector3(0, 5, 385);
                metralladoraFija.Scale = new Vector3(0.75f, 0.75f, 0.75f);

                metralladoras = new List<TgcMesh>();

                var instanceMetra1 = metralladoraFija.createMeshInstance("metra1");
                instanceMetra1.AutoTransformEnable = true;
                instanceMetra1.Position = new Vector3(-175, 5, -700);
                instanceMetra1.Scale = new Vector3(0.75f, 0.75f, 0.75f);
                instanceMetra1.rotateY((float)Math.PI);
                metralladoras.Add(instanceMetra1);

            } 
            //paredes        
            { scenePared = loader.loadSceneFromFile(
                this.MediaDir + "ModelosTgc\\Pared\\ParedBlanca-TgcScene.xml");
            pared = scenePared.Meshes[0];
            pared.Position = new Vector3(40, 0, -105);
            pared.Scale = new Vector3(0.2f, 0.2f, 1.6f);
                //instancias de pared
                {
                    paredes = new List<TgcMesh>();

                    var instancePared1 = pared.createMeshInstance("pared1");
                    instancePared1.AutoTransformEnable = true;
                    instancePared1.Position = new Vector3(40, 0, -418);
                    instancePared1.Scale = new Vector3(0.2f, 0.2f, 1.6f);

                    var instancePared2 = pared.createMeshInstance("pared2");
                    instancePared2.AutoTransformEnable = true;
                    instancePared2.Position = new Vector3(-460, 0, -418);
                    instancePared2.Scale = new Vector3(0.2f, 0.2f, 1.6f);

                    var instancePared3 = pared.createMeshInstance("pared3");
                    instancePared3.AutoTransformEnable = true;
                    instancePared3.Position = new Vector3(-460, 0, -105);
                    instancePared3.Scale = new Vector3(0.2f, 0.2f, 1.6f);

                    var instancePared4 = pared.createMeshInstance("pared4");
                    instancePared4.AutoTransformEnable = true;
                    instancePared4.rotateY((float)Math.PI / 2);
                    instancePared4.Position = new Vector3(-47, 0, -540);
                    instancePared4.Scale = new Vector3(0.2f, 0.2f, 1.2f);

                    var instancePared5 = pared.createMeshInstance("pared5");
                    instancePared5.AutoTransformEnable = true;
                    instancePared5.rotateY((float)Math.PI / 2);
                    instancePared5.Position = new Vector3(-373, 0, -540);
                    instancePared5.Scale = new Vector3(0.2f, 0.2f, 1.2f);

                    var instancePared6 = pared.createMeshInstance("pared6");
                    instancePared6.AutoTransformEnable = true;
                    instancePared6.rotateY((float)Math.PI / 2);
                    instancePared6.Position = new Vector3(-373, 0, 17);
                    instancePared6.Scale = new Vector3(0.2f, 0.2f, 1.2f);

                    var instancePared7 = pared.createMeshInstance("pared7");
                    instancePared7.AutoTransformEnable = true;
                    instancePared7.rotateY((float)Math.PI / 2);
                    instancePared7.Position = new Vector3(-47, 0, 17);
                    instancePared7.Scale = new Vector3(0.2f, 0.2f, 1.2f);

                    paredes.Add(instancePared1);
                    paredes.Add(instancePared2);
                    paredes.Add(instancePared3);
                    paredes.Add(instancePared4);
                    paredes.Add(instancePared5);
                    paredes.Add(instancePared6);
                    paredes.Add(instancePared7);
                }
            }
            //helicoptero
            {
                sceneHelicoptero = loader.loadSceneFromFile(
                  this.MediaDir + "MeshCreator\\Meshes\\Vehiculos\\HelicopteroMilitar\\HelicopteroMilitar-TgcScene.xml");
                helicoptero = sceneHelicoptero.Meshes[0];
                helicoptero.Position = new Vector3(-900, 5, -110);
                helicoptero.rotateY((float)Math.PI*3/2);


            }
            //aviones
            {
                sceneAvion = loader.loadSceneFromFile(
                      this.MediaDir + "MeshCreator\\Meshes\\Vehiculos\\AvionMilitar\\AvionMilitar-TgcScene.xml");
                avion = sceneAvion.Meshes[0];
                avion.Position = new Vector3(-362, 1000, -869);
                avion.rotateY((float)Math.PI);

                aviones = new List<TgcMesh>();
                var instanceAvion1 = avion.createMeshInstance("avion1");
                instanceAvion1.AutoTransformEnable = true;
                instanceAvion1.Position = new Vector3(308, 1000, 605);
                aviones.Add(instanceAvion1);
            }
            //hammer
            {
                sceneHammer = loader.loadSceneFromFile(
               this.MediaDir + "MeshCreator\\Meshes\\Vehiculos\\Hummer\\Hummer-TgcScene.xml");
                hammer = sceneHammer.Meshes[0];
                hammer.Position = new Vector3(278, 5, -540);
                hammer.rotateY((float)Math.PI / 4);
            }
            //Device de DirectX para crear primitivas.
            {
                var d3dDevice = D3DDevice.Instance.Device;

                //Camara en 1ra persona
                Camara = new TgcFpsCamera(new Vector3(0, 1500, 0), 600, 500, Input);
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
            //Renderizar instancias
            foreach (var mesh in paredes)
            {
                mesh.render();
            }
            foreach (var mesh in arboles1)
            {
                mesh.render();
            }
            foreach (var mesh in arboles2)
            {
                mesh.render();
            }
            foreach (var mesh in arboles3)
            {
                mesh.render();
            }
            foreach (var mesh in arboles4)
            {
                mesh.render();
            }
            foreach (var mesh in arboles5)
            {
                mesh.render();
            }
            foreach (var mesh in arboles6)
            {
                mesh.render();
            }
            foreach (var mesh in arboles7)
            {
                mesh.render();
            }
            foreach (var mesh in arboles8)
            {
                mesh.render();
            }
            foreach (var mesh in arboles9)
            {
                mesh.render();
            }
            foreach (var mesh in arboles10)
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
            foreach(var mesh in aviones)
            {
                mesh.render();
            }
            //Preparar ciudad
            foreach (var mesh in scene.Meshes)
            {
                mesh.render();
            }

            fuenteAgua.render();
            metralladoraFija.render();
            pared.render();
            cajaMunicion.render();
            camioneta.render();
            patrullero.render();
            barril.render();
            helicoptero.render();
            avion.render();
            hammer.render();
            logo.render();
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
            //Liberar ciudad
            scene.disposeAll();
            pasto.dispose();
            fuenteAgua.dispose();
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
        }
    }
}