using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "FontNames.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Retrieve all fonts used in the document via the instance FontUtilities
            Font[] fonts = doc.FontUtilities.GetAllFonts();

            List<string> fontNames = new List<string>();
            foreach (Font font in fonts)
            {
                // Font.FontName holds the font family name
                fontNames.Add(font.FontName);
            }

            // Save the collected font names to a text file
            File.WriteAllLines(outputPath, fontNames);
        }

        Console.WriteLine($"Font names saved to '{outputPath}'.");
    }
}
