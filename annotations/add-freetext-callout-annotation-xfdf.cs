using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // XFDF defining a FreeText annotation with a callout line
        string xfdf = @"<?xml version='1.0' encoding='UTF-8'?>
<xfdf xmlns='http://ns.adobe.com/xfdf/'>
  <annots>
    <freeText page='1' rect='100,500,300,600' contents='Sample callout'>
      <callout>
        <point x='150' y='550'/>
        <point x='200' y='560'/>
        <point x='250' y='570'/>
      </callout>
    </freeText>
  </annots>
</xfdf>";

        // Load the PDF, import the XFDF annotation, and save
        using (Document doc = new Document(inputPath))
        {
            // Correct method name: ImportAnnotationsFromXfdf (lower‑case "f")
            doc.ImportAnnotationsFromXfdf(xfdf);
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with FreeText callout annotation to '{outputPath}'.");
    }
}
