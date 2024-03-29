﻿using DevExpress.Xpf.Diagram;
using System;
using System.IO;
using System.Windows;

namespace DiagramSVGItemsWpf {
    public partial class MainWindow : Window {
        private string path;
        public MainWindow() {
            InitializeComponent();
            path = @"..\..\Icons";
            CreateToolboxItems(path);
        }

        void CreateToolboxItems(string path) {
            var stencil = new DevExpress.Diagram.Core.DiagramStencil("SVGStencil", "IcoMoon - Free Shapes");
            string name = string.Empty;
            foreach (string file in Directory.GetFiles(path)) {
                try {
                    name = Path.GetFileNameWithoutExtension(file);
                    using (FileStream stream = File.Open(file, FileMode.Open)) {
                        var shape = DevExpress.Diagram.Core.ShapeDescription.CreateSvgShape(name, name, stream)
                            .Update(getDefaultSize: () => new Size(100, 100))
                            .Update(getConnectionPoints: (w, h, p) => new[] { new Point(w / 2, h / 2) });
                        stencil.RegisterShape(shape);
                    }
                } catch (Exception) {
                    // some SVG files cannot be parsed
                }
            }
            DevExpress.Diagram.Core.DiagramToolboxRegistrator.RegisterStencil(stencil);
            diagramDesignerControl1.SelectedStencils.Add("SVGStencil");


            diagramDesignerControl1.Items.Add(new DiagramShape() { Shape = stencil.GetShape("aid-kit"), Width = 100, Height = 100, Position = new Point(300, 300) });
        }
    }
}
