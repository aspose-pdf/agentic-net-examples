using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for Field type

class PdfBatchFiller
{
    // Fills the same set of form fields in multiple PDF files concurrently.
    // inputFiles: full paths of source PDFs.
    // outputDir: directory where filled PDFs will be saved.
    // fieldValues: dictionary where key = field name, value = text to set.
    public static void FillFieldsInBatch(IEnumerable<string> inputFiles, string outputDir, Dictionary<string, string> fieldValues)
    {
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Parallel.ForEach will process files on multiple threads.
        Parallel.ForEach(inputFiles, inputPath =>
        {
            // Ensure the source file exists.
            if (!File.Exists(inputPath))
                return;

            // Derive output file name.
            string outputPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(inputPath) + "_filled.pdf");

            // Each PDF is handled in its own using block (lifecycle rule).
            using (Document doc = new Document(inputPath))
            {
                // Disable automatic recalculation for performance when many fields are set.
                doc.Form.AutoRecalculate = false;

                // Set each field value.
                foreach (var kvp in fieldValues)
                {
                    // If the field exists, assign the value using the Field object.
                    if (doc.Form.HasField(kvp.Key))
                    {
                        // The indexer returns a WidgetAnnotation; cast it to Field to access the Value property.
                        if (doc.Form[kvp.Key] is Field field)
                        {
                            field.Value = kvp.Value;
                        }
                    }
                }

                // Re‑enable recalculation if needed (optional).
                doc.Form.AutoRecalculate = true;

                // Save the modified PDF.
                doc.Save(outputPath);
            }
        });
    }

    // Example usage.
    static void Main()
    {
        // List of PDFs to process.
        var pdfFiles = new List<string>
        {
            "Invoice1.pdf",
            "Invoice2.pdf",
            "Invoice3.pdf"
        };

        // Output directory for filled PDFs.
        string outputDirectory = "FilledPdfs";

        // Fields to fill (same for all documents).
        var fields = new Dictionary<string, string>
        {
            { "CustomerName", "Acme Corp" },
            { "Date", DateTime.Today.ToShortDateString() },
            { "PreparedBy", "John Doe" }
        };

        FillFieldsInBatch(pdfFiles, outputDirectory, fields);

        Console.WriteLine("Batch fill completed.");
    }
}
