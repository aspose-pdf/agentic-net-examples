using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted_input.pdf";
        const string outputPath = "modified_output.pdf";
        const string password   = "userPassword"; // password for the encrypted PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF by providing the password to the Document constructor.
            Document pdfDoc = new Document(inputPath, password);

            // 1. Delete the first page (if it exists).
            if (pdfDoc.Pages.Count >= 1)
                pdfDoc.Pages.Delete(1);

            // 2. Rotate the (new) first page by 90 degrees clockwise.
            if (pdfDoc.Pages.Count >= 1)
                pdfDoc.Pages[1].Rotate = Rotation.on90; // use the correct enum value

            // 3. Insert a blank page at the end.
            // Create a temporary blank PDF document with a single empty page.
            Document blankDoc = new Document();
            blankDoc.Pages.Add(); // adds an empty page
            // Insert the blank page after the last existing page of the original document.
            pdfDoc.Pages.Insert(pdfDoc.Pages.Count + 1, blankDoc.Pages[1]);

            // Save the modified PDF.
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
