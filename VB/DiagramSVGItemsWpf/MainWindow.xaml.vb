﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Core
Imports System.IO
Imports DevExpress.Xpf.Diagram
Imports DevExpress.Diagram.Core

Namespace DiagramSVGItemsWpf
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits DXWindow

		Private path As String
		Public Sub New()
			InitializeComponent()
			path = "..\..\Icons"
			CreateToolboxItems(path)
		End Sub

		Private Sub CreateToolboxItems(ByVal path As String)
			Dim stencil = New DevExpress.Diagram.Core.DiagramStencil("SVGStencil", "IcoMoon - Free Shapes")
'INSTANT VB NOTE: The variable name was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim name_Renamed As String = String.Empty
			For Each file As String In Directory.GetFiles(path)
				Try
					name_Renamed = System.IO.Path.GetFileNameWithoutExtension(file)
					Using stream As FileStream = System.IO.File.Open(file, FileMode.Open)
'INSTANT VB WARNING: Instant VB cannot determine whether both operands of this division are integer types - if they are then you should use the VB integer division operator:
						Dim shape = DevExpress.Diagram.Core.ShapeDescription.CreateSvgShape(name_Renamed, name_Renamed, stream).Update(getDefaultSize:= Function() New System.Windows.Size(100, 100)).Update(getConnectionPoints:= Function(w, h, p) { New System.Windows.Point(w / 2, h / 2) })
						stencil.RegisterShape(shape)
					End Using
				Catch e1 As Exception
					'some SVG files cannot be parsed, so swallow the exception for now.
					'throw;
				End Try
			Next file
			DevExpress.Diagram.Core.DiagramToolboxRegistrator.RegisterStencil(stencil)
			diagramDesignerControl1.SelectedStencils.Add("SVGStencil")


			diagramDesignerControl1.Items.Add(New DiagramShape() With {.Shape = stencil.GetShape("aid-kit"), .Width = 100, .Height = 100, .Position = New Point(300,300)})
		End Sub
	End Class
End Namespace
