using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfFormFiller
{
    /// <summary>
    /// Fills the form fields of a PDF file using the provided field values and saves the result.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF containing form fields.</param>
    /// <param name="fieldValues">Dictionary where the key is the fully‑qualified field name and the value is the data to fill.</param>
    /// <param name="outputPath">Path where the filled PDF will be saved.</param>
    public static void FillPdfForm(string pdfPath, Dictionary<string, object> fieldValues, string outputPath)
    {
        // Ensure the source file exists
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"Source PDF not found: {pdfPath}");

        // Use the Form facade to work with AcroForm fields
        using (var form = new Form(pdfPath))
        {
            // Iterate over each field/value pair and apply the appropriate FillField overload
            foreach (KeyValuePair<string, object> kvp in fieldValues)
            {
                string fieldName = kvp.Key;
                object value = kvp.Value;

                if (value is string s)
                {
                    // Text field
                    form.FillField(fieldName, s);
                }
                else if (value is bool b)
                {
                    // Check box field
                    form.FillField(fieldName, b);
                }
                else if (value is int i)
                {
                    // Radio button, combo box or list box (by index)
                    form.FillField(fieldName, i);
                }
                else if (value is string[] arr)
                {
                    // List box with multiple selections
                    form.FillField(fieldName, arr);
                }
                else if (value != null)
                {
                    // Fallback: convert to string and fill as text
                    form.FillField(fieldName, value.ToString());
                }
                else
                {
                    // Null value – treat as empty string
                    form.FillField(fieldName, string.Empty);
                }
            }

            // Save the modified document to the desired output location
            form.Save(outputPath);
        }
    }
}

public class Program
{
    /// <summary>
    /// Minimal entry point required for compilation. Demonstrates how to call PdfFormFiller.
    /// </summary>
    public static void Main(string[] args)
    {
        // Expected arguments: <pdfPath> <outputPath> <fieldName>=<value> [more pairs...]
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <pdfPath> <outputPath> <fieldName>=<value> [more pairs...]");
            return;
        }

        string pdfPath = args[0];
        string outputPath = args[1];
        var fieldValues = new Dictionary<string, object>();

        for (int i = 2; i < args.Length; i++)
        {
            var pair = args[i].Split(new[] { '=' }, 2);
            if (pair.Length == 2)
            {
                // Simple parsing – everything after '=' is treated as a string value.
                fieldValues[pair[0]] = pair[1];
            }
        }

        try
        {
            PdfFormFiller.FillPdfForm(pdfPath, fieldValues, outputPath);
            Console.WriteLine($"Form filled successfully. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
