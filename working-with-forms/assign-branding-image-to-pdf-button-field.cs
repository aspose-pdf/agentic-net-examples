using System;
using System.IO;
using System.Drawing; // for System.Drawing.Image
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string imagePath = "brand.png";        // branding image
        const string outputPath = "output.pdf";      // result PDF
        const string buttonFieldName = "BrandButton";// name of the button field

        if (!File.Exists(pdfPath) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Required input files are missing.");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Open the image file as a stream (will be used for both XFA and AcroForm approaches)
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // ----- XFA form handling -----
                // If the PDF contains an XFA form, set the image directly on the field.
                if (doc.Form != null && doc.Form.XFA != null)
                {
                    // XFA.SetFieldImage(string fieldName, Stream image)
                    doc.Form.XFA.SetFieldImage(buttonFieldName, imgStream);
                }
                else if (doc.Form != null) // ----- AcroForm button handling -----
                {
                    // Retrieve the button field from the AcroForm collection.
                    var field = doc.Form[buttonFieldName] as ButtonField;
                    if (field != null)
                    {
                        // Reset stream position because it may have been read by XFA above.
                        imgStream.Position = 0;

                        // System.Drawing.Image is required by ButtonField.AddImage overload.
                        using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(imagePath))
                        {
                            // Add the image to the button field's appearance.
                            field.AddImage(sysImg);
                        }
                    }
                }
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPath}'.");
    }
}
