using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // XForm lives in this namespace
using Aspose.Pdf.Drawing;   // Matrix for appearance transformation

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_field.pdf";
        const string fieldName = "MyTextField"; // name of the form field to rotate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name; cast to TextBoxField (or other field type as needed)
            TextBoxField field = doc.Form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or not a TextBoxField.");
                return;
            }

            // The Appearance property is an AppearanceDictionary keyed by state names:
            // "N" – Normal, "R" – Rollover, "D" – Down.
            // To rotate the visual representation we need the XForm for the normal state.
            AppearanceDictionary appearances = field.Appearance;
            if (appearances != null && appearances.TryGetValue("N", out XForm normalForm))
            {
                // Rotate the appearance by 90 degrees clockwise using a manually built matrix.
                // PDF matrix layout: [a b c d e f] where x' = a*x + c*y + e, y' = b*x + d*y + f.
                // 90° clockwise => a=0, b=-1, c=1, d=0, e=0, f=0
                Aspose.Pdf.Matrix rotationMatrix = new Aspose.Pdf.Matrix(new double[] { 0, -1, 1, 0, 0, 0 });
                normalForm.Matrix = rotationMatrix;

                Console.WriteLine($"Rotation matrix applied (90° clockwise) for field '{fieldName}'.");
            }
            else
            {
                Console.Error.WriteLine($"Normal appearance stream not available for field '{fieldName}'.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rotated field to '{outputPath}'.");
    }
}
