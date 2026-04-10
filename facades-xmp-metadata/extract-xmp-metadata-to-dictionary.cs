using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

namespace XmpMetadataHelper
{
    // Helper class to extract XMP metadata from a PDF and convert it to a dictionary.
    public static class XmpHelper
    {
        // Public method that returns a dictionary with string keys and object values.
        // Complex XMP values (structures, arrays) are converted to nested dictionaries or lists.
        public static Dictionary<string, object> GetXmpMetadataDictionary(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            // Use PdfXmpMetadata facade to bind the PDF and access its XMP dictionary.
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(pdfPath); // Bind the PDF file.

                var result = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

                // PdfXmpMetadata implements IDictionary<string, XmpValue>.
                foreach (KeyValuePair<string, XmpValue> kvp in xmp)
                {
                    result[kvp.Key] = ConvertXmpValue(kvp.Value);
                }

                return result;
            }
        }

        // Recursive conversion of XmpValue to a plain .NET object.
        private static object ConvertXmpValue(XmpValue value)
        {
            if (value == null)
                return null;

            if (value.IsArray)
            {
                // Convert each element of the array.
                XmpValue[] array = value.ToArray();
                var list = new List<object>(array.Length);
                foreach (XmpValue item in array)
                {
                    list.Add(ConvertXmpValue(item));
                }
                return list;
            }

            if (value.IsStructure)
            {
                // Convert structure to a dictionary of its named values.
                var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                foreach (KeyValuePair<string, XmpValue> innerKvp in value.ToDictionary())
                {
                    dict[innerKvp.Key] = ConvertXmpValue(innerKvp.Value);
                }
                return dict;
            }

            if (value.IsDateTime)
                return value.ToDateTime();

            if (value.IsDouble)
                return value.ToDouble();

            if (value.IsInteger)
                return value.ToInteger();

            if (value.IsString)
                return value.ToStringValue();

            // Fallback to string representation for any other type.
            return value.ToString();
        }
    }

    // Example usage.
    class Program
    {
        static void Main()
        {
            const string pdfFile = "sample.pdf";

            try
            {
                Dictionary<string, object> xmpData = XmpHelper.GetXmpMetadataDictionary(pdfFile);

                Console.WriteLine("XMP Metadata extracted:");
                PrintDictionary(xmpData, 0);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error extracting XMP metadata: {ex.Message}");
            }
        }

        // Helper method to pretty‑print the dictionary (handles nested structures).
        private static void PrintDictionary(IDictionary<string, object> dict, int indent)
        {
            string indentStr = new string(' ', indent * 2);
            foreach (var kvp in dict)
            {
                if (kvp.Value is IDictionary<string, object> nestedDict)
                {
                    Console.WriteLine($"{indentStr}{kvp.Key}:");
                    PrintDictionary(nestedDict, indent + 1);
                }
                else if (kvp.Value is IEnumerable<object> list && !(kvp.Value is string))
                {
                    Console.WriteLine($"{indentStr}{kvp.Key}: [");
                    foreach (var item in list)
                    {
                        if (item is IDictionary<string, object> innerDict)
                        {
                            PrintDictionary(innerDict, indent + 2);
                        }
                        else
                        {
                            Console.WriteLine($"{new string(' ', (indent + 2) * 2)}{item}");
                        }
                    }
                    Console.WriteLine($"{indentStr}]");
                }
                else
                {
                    Console.WriteLine($"{indentStr}{kvp.Key}: {kvp.Value}");
                }
            }
        }
    }
}