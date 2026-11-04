using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Copy the original PDF to the output location – the incremental update
        // will be written to this copy while preserving the original revisions.
        File.Copy(inputPath, outputPath, true);

        // Open the copy with read/write access so that Aspose.Pdf can append
        // an incremental update to the existing file. A parameterless Save()
        // performs the incremental update automatically.
        using (FileStream fs = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(fs))
        {
            // Retrieve the form field "CustomerName" and set its value.
            if (doc.Form["CustomerName"] is Field field)
            {
                field.Value = "John Doe";
            }

            // Save the document – without options this triggers an incremental update.
            doc.Save();
        }

        Console.WriteLine($"PDF saved with incremental updates to '{outputPath}'.");
    }
}
