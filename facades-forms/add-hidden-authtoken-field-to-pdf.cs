using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Facades namespace imported as required

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Generate a secure random token
        string authToken = GenerateAuthToken();

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add invisible text containing the token to each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                TextFragment tf = new TextFragment(authToken);
                tf.TextState.Invisible = true;               // make the text hidden
                tf.Position = new Position(10, 10);          // position near the bottom‑left
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden AuthToken added and saved to '{outputPath}'.");
    }

    // Generates a 256‑bit token and returns it as a hex string
    static string GenerateAuthToken()
    {
        byte[] tokenBytes = new byte[32]; // 256 bits
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(tokenBytes);
        }

        StringBuilder sb = new StringBuilder(tokenBytes.Length * 2);
        foreach (byte b in tokenBytes)
            sb.AppendFormat("{0:x2}", b);
        return sb.ToString();
    }
}