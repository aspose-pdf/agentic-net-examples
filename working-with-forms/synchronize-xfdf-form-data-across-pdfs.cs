using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF files to be synchronized
        string[] pdfInputs = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // XFDF (XML) file containing the form data
        const string xfdfPath = "formdata.xfdf";
        // Directory where the updated PDFs will be saved
        const string outputDir = "SyncedPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify the XFDF file exists
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Open the XFDF stream once and reuse it for each PDF
        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        {
            foreach (string inputPath in pdfInputs)
            {
                // Verify each PDF exists before processing
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"PDF not found: {inputPath}");
                    continue;
                }

                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Import field values from the XFDF (XML) into the document
                    XfdfReader.ReadFields(xfdfStream, doc);
                    // Reset the stream position for the next document
                    xfdfStream.Position = 0;

                    // Save the updated PDF to the output directory
                    string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));
                    doc.Save(outputPath);
                }
            }
        }

        Console.WriteLine("Form data synchronized across PDFs.");
    }
}