using System;
using System.IO;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Obtain the array of fonts (replace this placeholder with actual logic)
        Aspose.Pdf.Text.Font[] fonts = GetFonts();

        const string outputPath = "FontNames.txt";

        // Ensure the StreamWriter is disposed properly
        using (StreamWriter writer = new StreamWriter(outputPath, false))
        {
            // Iterate through each Font and write its FontName to the file
            foreach (Aspose.Pdf.Text.Font font in fonts)
            {
                writer.WriteLine(font.FontName);
            }
        }

        Console.WriteLine($"Font names have been written to '{outputPath}'.");
    }

    // Placeholder method – replace with real code that returns the Font array
    static Aspose.Pdf.Text.Font[] GetFonts()
    {
        // Example: return an empty array; implement actual retrieval as needed
        return new Aspose.Pdf.Text.Font[0];
    }
}