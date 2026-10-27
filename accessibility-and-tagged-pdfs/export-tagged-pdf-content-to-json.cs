using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class TaggedElementDto
{
    // Initialise with empty strings to satisfy the non‑nullable warning.
    public string ElementType { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string AlternativeText { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty; // kept for compatibility; will be empty if not supported
    public List<TaggedElementDto> Children { get; set; } = new List<TaggedElementDto>();
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "tagged_content.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdfPath))
            {
                // The IsTagged property does not exist in many Aspose.Pdf versions.
                // Use a null‑check on TaggedContent to determine whether the PDF is tagged.
                if (doc.TaggedContent == null)
                {
                    Console.WriteLine("The document is not a tagged PDF. No structure to export.");
                    File.WriteAllText(outputJsonPath, "[]");
                    return;
                }

                ITaggedContent taggedContent = doc.TaggedContent;
                StructureElement rootElement = taggedContent.RootElement; // no cast required

                TaggedElementDto rootDto = BuildDto(rootElement);

                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(rootDto, jsonOptions);
                File.WriteAllText(outputJsonPath, json);

                Console.WriteLine($"Tagged content exported to '{outputJsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    static TaggedElementDto BuildDto(StructureElement element)
    {
        var dto = new TaggedElementDto
        {
            ElementType = element.GetType().Name,
            Text = element.ActualText ?? string.Empty,
            AlternativeText = element.AlternativeText ?? string.Empty,
            // The Language property may not be present in older versions; guard with a try/catch.
            Language = GetLanguageSafely(element)
        };

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                dto.Children.Add(BuildDto(childStruct));
            }
        }

        return dto;
    }

    // Helper to read Language when the property exists; otherwise return empty string.
    static string GetLanguageSafely(StructureElement element)
    {
        try
        {
            // Some versions expose Language; if not, an exception is thrown.
            return element.Language ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
}
