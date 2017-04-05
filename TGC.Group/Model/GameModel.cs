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
        private TgcScene scene;
        private TgcScene scene1;
        private TgcScene scene2;
        private TgcScene scene3;
        private TgcScene scene4;
        private TgcScene scene5;
        private TgcScene scene6;
        private TgcScene scene7;
        private TgcScene scene8;
        private TgcMesh auto;
        private TgcScene sceneAuto;
        private TgcMesh camioneta;
        private TgcScene sceneCamioneta;
        private TgcMesh helicoptero;
        private TgcScene sceneHelicoptero;

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
            TgcSceneLoader loader = new TgcSceneLoader();

            //Cargar scene -> ciudad
            {
                scene = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");

                scene1 = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scene2 = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scene3 = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scene4 = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scene5 = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scene6 = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scene7 = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                scene8 = loader.loadSceneFromFile(
                    this.MediaDir + "MeshCreator\\Scenes\\Ciudad\\Ciudad-TgcScene.xml");
                foreach (var mesh in scene1.Meshes)
                {
                    mesh.Position += new Vector3(2000, 0, 0);
                }
                foreach (var mesh in scene2.Meshes)
                {
                    mesh.Position += new Vector3(2000, 0, 2000);
                }
                foreach (var mesh in scene3.Meshes)
                {
                    mesh.Position += new Vector3(0, 0, 2000);
                }
                foreach (var mesh in scene4.Meshes)
                {
                    mesh.Position += new Vector3(-2000, 0, 2000);
                }
                foreach (var mesh in scene5.Meshes)
                {
                    mesh.Position += new Vector3(-2000, 0, 0);
                }
                foreach (var mesh in scene6.Meshes)
                {
                    mesh.Position += new Vector3(-2000, 0, -2000);
                }
                foreach (var mesh in scene7.Meshes)
                {
                    mesh.Position += new Vector3(0, 0, -2000);
                }
                foreach (var mesh in scene8.Meshes)
                {
                    mesh.Position += new Vector3(2000, 0, -2000);
                }
            }


            //Cargar mesh -> auto
            sceneAuto =
                loader.loadSceneFromFile(MediaDir +
                                         "MeshCreator\\Meshes\\Vehiculos\\Auto\\Auto-TgcScene.xml");
            auto = sceneAuto.Meshes[0];

            //Posicionar auto
            
            
            //Cargar mesh -> camioneta
            sceneCamioneta =
                loader.loadSceneFromFile(MediaDir +
                                         "MeshCreator\\Meshes\\Vehiculos\\Camioneta\\Camioneta-TgcScene.xml");
            camioneta = sceneCamioneta.Meshes[0];

            //posicionar camioneta
            camioneta.move(new Vector3(0, 0, 250));

            //Cargar mesh -> helicoptero
            sceneHelicoptero =
                loader.loadSceneFromFile(MediaDir +
                                         "MeshCreator\\Meshes\\Vehiculos\\HelicopteroMilitar\\HelicopteroMilitar-TgcScene.xml");
            helicoptero = sceneHelicoptero.Meshes[0];

            //posicionar helicoptero
            helicoptero.move(new Vector3(0, 720, 650));


            //Device de DirectX para crear primitivas.
            var d3dDevice = D3DDevice.Instance.Device;

            //FPS Camara
            Camara = new TgcFpsCamera(new Vector3(0, 1500, 0), 600f, 600f, Input);
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

            //Preparar ciudad
            {
                foreach (var mesh in scene.Meshes)
                {
                    mesh.render();
                }
                foreach (var mesh in scene1.Meshes)
                {
                    mesh.render();
                }
                foreach (var mesh in scene2.Meshes)
                {
                    mesh.render();
                }
                foreach (var mesh in scene3.Meshes)
                {
                    mesh.render();
                }
                foreach (var mesh in scene4.Meshes)
                {
                    mesh.render();
                }
                foreach (var mesh in scene5.Meshes)
                {
                    mesh.render();
                }
                foreach (var mesh in scene6.Meshes)
                {
                    mesh.render();
                }
                foreach (var mesh in scene7.Meshes)
                {
                    mesh.render();
                }
                foreach (var mesh in scene8.Meshes)
                {
                    mesh.render();
                }
            }

            //Mostrar coordenadas de camara
            DrawText.drawText(Camara.Position.ToString(), 150, 20, Color.Purple);

            //Preparar vehiculos
            auto.render();
            camioneta.render();
            helicoptero.render();

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
            scene1.disposeAll();
            scene2.disposeAll();
            scene3.disposeAll();
            scene4.disposeAll();
            scene5.disposeAll();
            scene6.disposeAll();
            scene7.disposeAll();
            scene8.disposeAll();

            //Liberar vehiculos
            auto.dispose();
            camioneta.dispose();
            helicoptero.dispose();
        }
    }
}