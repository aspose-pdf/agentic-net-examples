using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Embed a file into the PDF (use EmbeddedFiles + FileSpecification)
            // ------------------------------------------------------------
            using (FileStream fs = File.OpenRead(attachmentPath))
            {
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath), "Attachment");
                fileSpec.Contents = fs;               // set the file data
                doc.EmbeddedFiles.Add(fileSpec);       // add to the PDF
            }

            // ------------------------------------------------------------
            // 2. (Optional) Set viewer preferences – removed because the
            //    ViewerPreferences class/property is not available in the
            //    referenced Aspose.Pdf version.
            // ------------------------------------------------------------
            // var preferences = new ViewerPreferences
            // {
            //     HideToolbar = true,
            //     DisplayDocTitle = true
            // };
            // doc.ViewerPreferences = preferences;

            // ------------------------------------------------------------
            // 3. Define permissions and encrypt the document with a user password
            // ------------------------------------------------------------
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, null, permissions, CryptoAlgorithm.AESx256);

            // ------------------------------------------------------------
            // 4. Save the encrypted PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}
