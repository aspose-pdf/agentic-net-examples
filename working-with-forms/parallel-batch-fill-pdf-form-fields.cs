using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        string inputDirectory = "InputPdfs";
        // Directory where filled PDFs will be written
        string outputDirectory = "OutputPdfs";

        // Ensure both folders exist
        Directory.CreateDirectory(inputDirectory);
        Directory.CreateDirectory(outputDirectory);

        // Collect all PDF files to process (if any)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{Path.GetFullPath(inputDirectory)}'." +
                              " Place PDFs in this folder and rerun the program.");
            return;
        }

        // Example field values to apply to each document
        var fieldValues = new Dictionary<string, string>
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToShortDateString() }
        };

        // Parallel options – use as many cores as available
        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        // Process each PDF concurrently
        Parallel.ForEach(pdfFiles, parallelOptions, inputPath =>
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, $"{fileName}_filled.pdf");

            // Load the PDF, fill fields, and save
            using (Document doc = new Document(inputPath))
            {
                // Disable automatic recalculation for better performance
                doc.Form.AutoRecalculate = false;

                // Set each field value if the field exists in the form
                foreach (var kvp in fieldValues)
                {
                    if (doc.Form.HasField(kvp.Key))
                    {
                        // The Form indexer returns a WidgetAnnotation; cast it to Field
                        Field? field = doc.Form[kvp.Key] as Field;
                        if (field != null)
                        {
                            field.Value = kvp.Value;
                        }
                    }
                }

                // Save the modified PDF (PDF format is implicit)
                doc.Save(outputPath);
            }
        });

        Console.WriteLine("Batch field filling completed.");
    }
}
