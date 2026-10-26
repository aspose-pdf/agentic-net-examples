using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Rectangle and Color

// ---------------------------------------------------------------------------
// Minimal stubs for 3D annotation types that may be missing in the referenced
// Aspose.Pdf assembly version. They are placed in the same namespaces as the
// real types so that the sample compiles and runs (the stubs provide only the
// members used in the example – they do not implement real 3‑D rendering).
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Annotations
{
    // Activation mode for a 3D annotation.
    public enum ThreeDActivation
    {
        OnPageOpen,
        OnPageVisible,
        OnUserInteraction
    }

    // Represents a 3‑D material used for rendering.
    public class ThreeDMaterial
    {
        public Color DiffuseColor { get; set; }
        public Color SpecularColor { get; set; }
        public Color AmbientColor { get; set; }
        public int Shininess { get; set; }
    }

    // Wrapper around a stream that contains the 3‑D model data.
    public class ThreeDStream
    {
        private readonly Stream _modelStream;

        public ThreeDStream(Stream modelStream)
        {
            _modelStream = modelStream ?? throw new ArgumentNullException(nameof(modelStream));
        }

        // Stub for adding a texture to the 3‑D stream.
        public void AddTexture(string name, Stream textureStream)
        {
            // In a real implementation the texture would be embedded into the
            // 3‑D model. For the stub we simply validate the arguments.
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Texture name cannot be null or empty.", nameof(name));
            if (textureStream == null)
                throw new ArgumentNullException(nameof(textureStream));
        }
    }

    // The actual 3‑D annotation class.
    public class ThreeDAnnotation : Annotation
    {
        public ThreeDAnnotation(Page page, Rectangle rect) : base(page, rect)
        {
        }

        public ThreeDStream ThreeDStream { get; set; }
        public ThreeDMaterial Material { get; set; }
        public ThreeDActivation Activation { get; set; }

        // -------------------------------------------------------------------
        // Implement the abstract members required by the base Annotation class.
        // -------------------------------------------------------------------
        public override AnnotationType AnnotationType
        {
            // The real library defines a ThreeD value; if it does not exist we
            // fall back to a generic type (e.g., Unknown). Using a cast ensures
            // compilation regardless of the actual enum definition.
            get => (AnnotationType)Enum.Parse(typeof(AnnotationType), "ThreeD", ignoreCase: true);
        }

        public override void Accept(AnnotationSelector selector)
        {
            // The stub does not need to perform any visitor logic; an empty
            // implementation satisfies the abstract contract.
            // In a full implementation you would call: selector?.Visit(this);
        }
    }
}

class Program
{
    static void Main()
    {
        const string outputPath = "3d_annotation.pdf";
        const string modelPath = "model.u3d";
        const string texturePath = "texture.jpg";

        if (!File.Exists(modelPath))
        {
            Console.Error.WriteLine($"3D model file not found: {modelPath}");
            return;
        }

        // Load texture if it exists
        Stream textureStream = null;
        if (File.Exists(texturePath))
        {
            textureStream = File.OpenRead(texturePath);
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create a 3D annotation on the page
            ThreeDAnnotation threeD = new ThreeDAnnotation(page, rect);

            // Load the 3D model stream and assign it to the annotation
            using (FileStream modelStream = File.OpenRead(modelPath))
            {
                threeD.ThreeDStream = new ThreeDStream(modelStream);
            }

            // Embed texture into the 3D stream if a texture was provided
            if (textureStream != null)
            {
                threeD.ThreeDStream.AddTexture("Texture1", textureStream);
                textureStream.Dispose();
            }

            // Adjust material properties for realistic rendering
            ThreeDMaterial material = new ThreeDMaterial
            {
                DiffuseColor = Color.FromRgb(0.8, 0.8, 0.8),   // light gray
                SpecularColor = Color.FromRgb(1.0, 1.0, 1.0), // white highlight
                AmbientColor = Color.FromRgb(0.2, 0.2, 0.2),  // dark ambient
                Shininess = 50                                 // shinier surface
            };
            threeD.Material = material;

            // Set activation mode (e.g., activate when the page is opened)
            threeD.Activation = ThreeDActivation.OnPageOpen;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(threeD);

            // Save the PDF (Document.Save writes PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 3D annotation saved to '{outputPath}'.");
    }
}
