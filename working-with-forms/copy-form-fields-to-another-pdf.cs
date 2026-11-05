using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string destinationPath = "destination.pdf";
        const string outputPath = "merged_with_forms.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }

        try
        {
            // Load source and destination PDFs
            using (Document srcDoc = new Document(sourcePath))
            using (Document dstDoc = new Document(destinationPath))
            {
                // Destination document already contains a Form (AcroForm) accessible via the Form property
                Form dstForm = dstDoc.Form;

                // Iterate over each field in the source document and add it to the destination form
                foreach (Field srcField in srcDoc.Form)
                {
                    // Add creates a copy of the field appropriate for the target document
                    dstForm.Add(srcField);
                }

                // Save the merged PDF
                dstDoc.Save(outputPath);
            }

            Console.WriteLine($"Form fields copied successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
