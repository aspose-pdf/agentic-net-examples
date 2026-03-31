using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "output.pdf";
        const string password = "userpass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted document by providing the password
            using (Document document = new Document(inputPath, password))
            {
                // Delete the first page if it exists
                if (document.Pages.Count >= 1)
                {
                    document.Pages.Delete(1);
                }

                // Rotate the (new) second page by 90 degrees if it exists
                if (document.Pages.Count >= 2)
                {
                    Page page = document.Pages[2];
                    page.Rotate = Rotation.on90; // rotate 90 degrees clockwise
                }

                // Save the modified PDF
                document.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
