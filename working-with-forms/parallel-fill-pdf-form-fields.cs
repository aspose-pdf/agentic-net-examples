using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        string inputFolder = "InputPdfs";
        string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Gather all PDF files to process
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        // Process each PDF in parallel to improve batch performance
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Each PDF gets its own Document instance wrapped in a using block (lifecycle rule)
                using (Document doc = new Document(pdfPath))
                {
                    // Example: fill a text field named "Name" with the file name (without extension)
                    const string fieldName = "Name";
                    string fieldValue = Path.GetFileNameWithoutExtension(pdfPath);

                    // Ensure the form exists and the field is present
                    if (doc.Form != null)
                    {
                        // The Form indexer returns a WidgetAnnotation; cast it to Field before using Value
                        Field? field = doc.Form[fieldName] as Field;
                        if (field != null)
                        {
                            field.Value = fieldValue; // Set the field value
                        }
                    }

                    // Save the modified PDF to the output directory
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {pdfPath}: {ex.Message}");
            }
        });

        Console.WriteLine("Batch processing completed.");
    }
}
