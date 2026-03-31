using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123"; // set to empty if password is unknown

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Document doc = null;
        try
        {
            // Attempt to open without a password first
            doc = new Document(inputPath);
        }
        catch (InvalidPasswordException)
        {
            // PDF is encrypted; try opening with the supplied password
            try
            {
                doc = new Document(inputPath, userPassword);
                // Decrypt the document so further modifications are allowed
                doc.Decrypt();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to open encrypted PDF: {ex.Message}");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error opening PDF: {ex.Message}");
            return;
        }

        using (doc)
        {
            // Modify the content – add a text fragment to the first page
            Page firstPage = doc.Pages[1];
            TextFragment textFragment = new TextFragment("Modified by Aspose.Pdf");
            textFragment.TextState.FontSize = 12;
            textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            textFragment.Position = new Position(100, 700);
            firstPage.Paragraphs.Add(textFragment);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF processed and saved as '{outputPath}'.");
    }
}