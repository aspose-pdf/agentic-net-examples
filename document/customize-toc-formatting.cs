using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Create a new TocInfo instance
            TocInfo tocInfo = new TocInfo();

            // Prepare LevelFormat array (one entry per TOC level you want to customize)
            LevelFormat[] levelFormats = new LevelFormat[2];

            // Level 1 – solid tab leader
            levelFormats[0] = new LevelFormat
            {
                LineDash = Aspose.Pdf.Text.TabLeaderType.Solid
            };

            // Level 2 – dot tab leader
            levelFormats[1] = new LevelFormat
            {
                LineDash = Aspose.Pdf.Text.TabLeaderType.Dot
            };

            // Assign the custom format array to the TocInfo
            tocInfo.FormatArray      = levelFormats;
            tocInfo.FormatArrayLength = levelFormats.Length;

            // Disable page numbers for the TOC (applies to the whole TOC)
            tocInfo.IsShowPageNumbers = false;

            // Attach the TocInfo to the first page (the page that will contain the TOC)
            doc.Pages[1].TocInfo = tocInfo;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"TOC customized and saved to '{outputPath}'.");
    }
}