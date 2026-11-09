using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Drawing;                     // System.Drawing.Rectangle is required by PdfContentEditor
using Aspose.Pdf.Facades;                // PdfContentEditor resides here

// Define a POCO that matches the JSON structure for an annotation
public class AnnotationDefinition
{
    public int    PageNumber { get; set; }   // 1‑based page index
    public int    X          { get; set; }   // lower‑left X coordinate (points)
    public int    Y          { get; set; }   // lower‑left Y coordinate (points)
    public int    Width      { get; set; }   // rectangle width (points)
    public int    Height     { get; set; }   // rectangle height (points)
    public string? Title      { get; set; }   // annotation title (shown in UI) – nullable to satisfy non‑nullable warning
    public string? Contents   { get; set; }   // annotation body text – nullable to satisfy non‑nullable warning
    public string? Icon       { get; set; }   // optional icon name (e.g., "Note") – nullable as it is optional
}

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "annotated_output.pdf";
        const string jsonPath       = "annotations.json";

        // Validate files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load annotation definitions from JSON
        List<AnnotationDefinition> annotations;
        try
        {
            string json = File.ReadAllText(jsonPath);
            annotations = JsonSerializer.Deserialize<List<AnnotationDefinition>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<AnnotationDefinition>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Initialize PdfContentEditor and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);

        // Apply each annotation definition
        foreach (var ann in annotations)
        {
            // Ensure page number is within range (PdfContentEditor uses 1‑based indexing)
            if (ann.PageNumber < 1)
            {
                Console.Error.WriteLine($"Invalid page number {ann.PageNumber}; skipping annotation.");
                continue;
            }

            // Build the rectangle for the annotation (System.Drawing.Rectangle expects X,Y,Width,Height)
            Rectangle rect = new Rectangle(ann.X, ann.Y, ann.Width, ann.Height);

            // Create a text annotation. PdfContentEditor.CreateText signature:
            // CreateText(Rectangle rect, string title, string contents, bool isOpen, string icon, int pageNumber)
            // We use default values for isOpen (false) and icon (empty string) when they are not supplied.
            try
            {
                bool isOpen = false; // default – annotation is closed
                string icon  = ann.Icon ?? string.Empty;
                editor.CreateText(
                    rect,
                    ann.Title   ?? string.Empty,
                    ann.Contents ?? string.Empty,
                    isOpen,
                    icon,
                    ann.PageNumber);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to add annotation on page {ann.PageNumber}: {ex.Message}");
            }
        }

        // Save the modified PDF
        editor.Save(outputPdfPath);
        Console.WriteLine($"Annotations applied and saved to '{outputPdfPath}'.");
    }
}
