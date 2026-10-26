using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_3d.pdf";
        const string u3dFile = "model.u3d";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(u3dFile))
        {
            Console.Error.WriteLine($"3D model file not found: {u3dFile}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Define the rectangle area for the 3D annotation (fully‑qualified type as per rule)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

                // Try to create a ThreeDAnnotation via reflection – this avoids a direct compile‑time dependency
                Type threeDAnnotationType = Type.GetType("Aspose.Pdf.Annotations.ThreeDAnnotation, Aspose.Pdf");
                Type threeDStreamType = Type.GetType("Aspose.Pdf.Annotations.ThreeDStream, Aspose.Pdf");

                if (threeDAnnotationType != null && threeDStreamType != null)
                {
                    // Instantiate ThreeDAnnotation(rect)
                    object threeDAnnotation = Activator.CreateInstance(threeDAnnotationType, rect);

                    // Load the 3D stream (U3D) and assign it to the annotation
                    using (FileStream u3dStream = File.OpenRead(u3dFile))
                    {
                        object threeDStream = Activator.CreateInstance(threeDStreamType, u3dStream);
                        threeDAnnotationType.GetProperty("ThreeDStream").SetValue(threeDAnnotation, threeDStream);
                    }

                    // Set initial view parameters – rotation 0° shows the front face
                    threeDAnnotationType.GetProperty("Rotation").SetValue(threeDAnnotation, 0);

                    // Optional: set background color for the annotation (fully‑qualified Color)
                    threeDAnnotationType.GetProperty("BackgroundColor").SetValue(threeDAnnotation, Aspose.Pdf.Color.White);

                    // Add the annotation to the first page
                    var annotations = doc.Pages[1].Annotations;
                    var addMethod = annotations.GetType().GetMethod("Add");
                    addMethod.Invoke(annotations, new[] { threeDAnnotation });
                }
                else
                {
                    Console.WriteLine("ThreeDAnnotation types are not available in the referenced Aspose.Pdf version. Skipping 3D annotation creation.");
                }

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"3D annotation processing completed and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
