using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create sample PDFs (evaluation mode allows up to 4 documents)
        List<string> inputFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string fileName = $"sample{i}.pdf";
            CreateSamplePdf(fileName, $"Sample PDF {i}");
            inputFiles.Add(fileName);
        }

        // Encryption settings
        string userPassword = "user123";
        string ownerPassword = "owner123";
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Encrypt each PDF and save as a new file
        foreach (string inputPath in inputFiles)
        {
            string encryptedPath = System.IO.Path.GetFileNameWithoutExtension(inputPath) + "_encrypted.pdf";
            using (Document doc = new Document(inputPath))
            {
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPath);
            }

            // NOTE: Applying a digital signature requires Aspose.Pdf.Facades (PdfFileSignature),
            // which is prohibited by the namespace restriction. Therefore, the signature step is omitted.
        }
    }

    static void CreateSamplePdf(string filePath, string text)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment(text);
            page.Paragraphs.Add(tf);
            doc.Save(filePath);
        }
    }
}