using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputFolder = "InputPdfs";
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(outputFolder, fileName);

            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(sourcePath))
            {
                var form = pdfDoc.Form;

                // Verify that the document contains a form and the "Comments" field exists
                if (form != null && form.Fields != null && form.Fields.Any(f => f.Name == "Comments"))
                {
                    // The Form indexer returns a WidgetAnnotation; cast it to Field first
                    Field field = form["Comments"] as Field;
                    if (field != null)
                    {
                        if (field is TextBoxField txtField)
                        {
                            txtField.MaxLen = 100; // set the 100‑character limit
                        }
                        else
                        {
                            Console.Error.WriteLine($"Field 'Comments' is not a text box in {fileName}");
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine($"Unable to cast 'Comments' to a form field in {fileName}");
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Field 'Comments' not found in {fileName}");
                }

                // Save the modified PDF to the target location
                pdfDoc.Save(targetPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }
    }
}
