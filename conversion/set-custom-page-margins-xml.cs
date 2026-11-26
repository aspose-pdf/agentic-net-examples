using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_custom_margin.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // XML defining custom margin values for the page
            string pageInfoXml = @"<?xml version='1.0' encoding='utf-8'?>
<PageInfo>
    <Margin>
        <Top>50</Top>
        <Bottom>50</Bottom>
        <Left>30</Left>
        <Right>30</Right>
    </Margin>
</PageInfo>";

            // Bind the XML to the document – this updates the PageInfo margins
            doc.BindXml(pageInfoXml);

            // Save the modified PDF (PDF output does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom margins to '{outputPath}'.");
    }
}