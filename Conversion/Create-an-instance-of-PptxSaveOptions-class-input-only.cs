using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new instance of PptxSaveOptions using its default constructor.
        PptxSaveOptions pptxOptions = new PptxSaveOptions();

        // Optional: configure a property. For example, enable glyph caching to improve performance.
        pptxOptions.CacheGlyphs = true;

        // Verify that the instance was created successfully.
        Console.WriteLine($"PptxSaveOptions created. CacheGlyphs = {pptxOptions.CacheGlyphs}");
    }
}