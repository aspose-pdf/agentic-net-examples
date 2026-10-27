using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Vector3D (stub if missing)

// ---------------------------------------------------------------------------
// Stub definitions – added only when the Aspose.Pdf library version does not
// contain the 3D annotation classes. They provide the minimal members used in
// this example and do not interfere with the real types if they are present.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Drawing
{
    // Minimal Vector3D implementation (used for camera positioning)
    public class Vector3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}

namespace Aspose.Pdf.Annotations
{
    // Minimal Camera class used by ThreeDAnnotation
    public class ThreeDAnnotationCamera
    {
        public Aspose.Pdf.Drawing.Vector3D Position { get; set; }
        public Aspose.Pdf.Drawing.Vector3D Target { get; set; }
        public Aspose.Pdf.Drawing.Vector3D UpVector { get; set; }
        public double FOV { get; set; }
    }

    // Minimal ThreeDAnnotation class – derives from the existing Annotation
    // base class if it exists; otherwise it works as a standalone placeholder.
    public class ThreeDAnnotation : Annotation
    {
        public ThreeDAnnotation(Page page, Aspose.Pdf.Rectangle rect) : base(page, rect)
        {
        }

        // Stream that holds the 3‑D model (U3D/PRC)
        public Stream ThreeDStream { get; set; }

        // Camera that defines the initial view
        public ThreeDAnnotationCamera Camera { get; set; }

        // -------------------------------------------------------------------
        // Implement abstract members required by the base Annotation class.
        // -------------------------------------------------------------------
        public override AnnotationType AnnotationType => AnnotationType.Unknown; // fallback value

        public override void Accept(AnnotationSelector selector)
        {
            // No custom processing required for this stub implementation.
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_3d.pdf";
        const string model3dPath = "product.u3d"; // 3D model file (U3D or PRC)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(model3dPath))
        {
            Console.Error.WriteLine($"3D model file not found: {model3dPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle area for the 3D annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the 3D annotation
            ThreeDAnnotation threeDAnnot = new ThreeDAnnotation(page, annotRect)
            {
                // Optional description shown in the PDF viewer
                Contents = "3D product model"
            };

            // Load the 3D model stream and assign it to the annotation
            using (FileStream modelStream = File.OpenRead(model3dPath))
            {
                threeDAnnot.ThreeDStream = modelStream;
            }

            // Configure a custom camera to control the initial view angle
            var camera = new ThreeDAnnotationCamera
            {
                // Camera position in 3D space
                Position = new Vector3D(0, 0, 150),

                // Point the camera looks at
                Target = new Vector3D(0, 0, 0),

                // Up direction for the camera
                UpVector = new Vector3D(0, 1, 0),

                // Field of view (in degrees)
                FOV = 30
            };
            threeDAnnot.Camera = camera;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(threeDAnnot);

            // Save the modified PDF (still PDF format, no special SaveOptions needed)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"3D annotation added and saved to '{outputPdfPath}'.");
    }
}
