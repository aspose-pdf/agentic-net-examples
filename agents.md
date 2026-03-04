# Aspose.PDF for .NET Examples

AI-friendly repository containing validated C# examples for Aspose.PDF for .NET API.

## Repository Overview

This repository contains **3** working code examples demonstrating Aspose.PDF for .NET capabilities. Examples are automatically generated, compiled, and validated using the Aspose.PDF Examples Generator.

**Statistics** (as of 2026-03-04):
- Total Examples: 3
- Categories: 1
- Pass Rate: 100.0%

## Repository Structure

Examples are organized by feature category:

```
/Accessibility-and-Tagged-PDFs/    - Tagged PDF creation, structure elements, accessibility
/Basic-Operations/                  - Splitting, merging, basic document operations
/Compare-PDF/                       - Document comparison features
/Conversion/                        - PDF conversion to/from various formats
/Document/                          - Document-level operations
/Facades-AcroForms/                 - PDF forms using Facades API
/Facades-Annotations/               - Annotations using Facades API
/Facades-Bookmarks/                 - Bookmark manipulation
/Facades-Convert-Documents/         - Document conversion via Facades
/Facades-Documents/                 - Document operations with Facades
/Facades-Extract-Images-and-Text/   - Content extraction
/Facades-Forms/                     - Form field manipulation
/Facades-Metadata/                  - Metadata operations
/Parse-PDF/                         - PDF parsing and extraction
/Stamping/                          - Text/image stamping
/Working-With-Text/                 - Text manipulation
/Working-with-Attachments/          - Attachment handling
/Working-with-XML/                  - XML-based operations
```

## Category Details

### Basic Operations
- Examples: 3
- Pass Rate: 3/3 (100%)
- Guide: [agents.md](./Basic Operations/agents.md)


## How to Use These Examples

### Prerequisites
- .NET SDK (version 10.0 or higher)
- Aspose.PDF for .NET (version 26.2.0 or higher)
- NuGet package restore enabled

### Running an Example
1. Navigate to any category folder
2. Each .cs file is a standalone Console Application
3. Ensure `input.pdf` exists in the same directory (where required)
4. Compile and run:
   ```bash
   dotnet run <example-file.cs>
   ```

### Build Instructions
All examples follow these patterns:
- **Document disposal**: `using (Document doc = new Document(...))` blocks
- **Error handling**: Try-catch with descriptive error messages
- **File checks**: Verify input files exist before processing
- **Generic filenames**: Examples use `input.pdf`, `output.pdf` for consistency

### Code Conventions
- **No `var` keyword**: All types explicitly declared
- **One-based indexing**: Aspose.PDF uses 1-based page indexing (`Pages[1]` = first page)
- **Deterministic cleanup**: All IDisposable objects wrapped in `using` blocks
- **Console output**: Success/error messages written to Console.WriteLine/Console.Error

## Common Patterns

### Loading a PDF
```csharp
using (Document pdfDoc = new Document("input.pdf"))
{
    // Work with document
}
```

### Error Handling
```csharp
if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"Error: File not found – {inputPath}");
    return;
}

try
{
    // Operations
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
}
```

### Page Iteration (1-based)
```csharp
for (int i = 1; i <= pdfDoc.Pages.Count; i++)
{
    Page page = pdfDoc.Pages[i];
    // Process page
}
```

## API Coverage

This repository covers:
- PDF creation and manipulation
- Format conversion (PDF ↔ DOCX, HTML, SVG, XPS, etc.)
- Annotations and bookmarks
- Forms (AcroForms and XFA)
- Text and image extraction
- Accessibility (tagged PDFs, structure elements)
- Document comparison
- Stamping and watermarking
- Metadata and properties
- Security and encryption
- Facades API (simplified operations)

## Example Naming Convention

Files use descriptive task-based names:
- `Category-Feature-specific-task-description-format.cs`
- Example: `Compare-PDF-documents-while-working-with-vector-graphics-ZUGFeRD-or-JavaScript-and-save-the-result-as-PDF.cs`

## Contributing

Examples in this repository are **automatically generated** by the Aspose.PDF Examples Generator tool.

To suggest new examples:
1. Submit tasks to the generator
2. Generated examples are validated via compilation
3. Passing examples are included in monthly batches

## Versioning & Updates

- Examples are updated monthly
- Each batch is submitted via pull request
- PRs include validation results (pass/fail status per example)
- Only compilation-validated examples are merged

## Related Resources

- [Aspose.PDF for .NET Documentation](https://docs.aspose.com/pdf/net/)
- [API Reference](https://reference.aspose.com/pdf/net/)
- [Aspose Forum](https://forum.aspose.com/c/pdf/10)

## License

All examples use Aspose.PDF for .NET and require a valid license for production use. See [licensing](https://purchase.aspose.com/pdf/net).

---

*This repository is maintained by automated code generation. Last updated: 2026-03-04 | Total examples: 3 | Pass rate: 100.0%*
