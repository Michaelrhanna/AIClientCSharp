# AIClient

A lightweight Windows desktop chat client built with C# and WinForms that connects to a locally hosted Shimmy instance + Phi-3 model over HTTP.

---

## Features

- Chat interface with conversation history
- Configurable system prompt via a setup dialog
- Phi-3 prompt formatting with correct SLM tokens
- Secure API key injection via `appsettings.json`
- Clean architecture with Dependency Injection
- Unit tested with xUnit v3 and Moq

---

## Requirements

- .NET 10
- Windows (WinForms)
- A running Shimmy instance with phi-3 model accessible over your local network

---

## Getting Started

### 1. Clone the repository

git clone https://github.com/your-username/AIClient.git
cd AIClient

### 2. Configure `appsettings.json`

Create an `appsettings.json` file in the project root with the following content:

json
{
  "ShimmyApiKey": "your-api-key-here",
  "ShimmyBaseUrl": "http://192.168.0.x:8000/api/generate",
  "ModelName": "your-model-name-here",
  "MaxTokens": 512,
  "HttpTimeoutSeconds": 300
}


| Setting              | Description                                    |
|----------------------|------------------------------------------------|
| `ShimmyApiKey`       | API key for your Shimmy instance               |
| `ShimmyBaseUrl`      | Full URL to your Shimmy generate endpoint      |
| `ModelName`          | Model name as configured in Shimmy             |
| `MaxTokens`          | Maximum tokens in the model response           |
| `HttpTimeoutSeconds` | HTTP request timeout in seconds (default: 300) |


## Usage

### Sending a message
Type your message in the input box at the bottom and click **Send** . The conversation history is displayed in the chat panel above.

### Setting up a system prompt
Click the **Setup** button to open the system prompt dialog. Enter your instructions for the model and click **Save**. 

> **Note:** Saving a new system prompt will clear the current conversation history.

---

## Project Structure

```
AIClient/
├── Forms/
│   ├── FormMain.cs           # Main chat window
│   └── FormSetup.cs          # System prompt setup dialog
├── Model/
│   ├── AppSettings.cs        # Configuration model
│   ├── APIKeyManager.cs      # API key injection
│   ├── ChatMessage.cs        # Message model
│   ├── ChatService.cs        # Conversation orchestration
│   ├── PayLoad.cs            # Shimmy request model
│   ├── Phi3Formatter.cs      # Phi-3 prompt formatter
│   ├── ShimmyProvider.cs     # Shimmy HTTP provider
│   └── ShimmyResponse.cs     # Shimmy response model
├── Model/Interface/
│   ├── IChatService.cs
│   ├── IPromptFormatter.cs
│   └── IProvider.cs
├── Utils/
│   └── Constants.cs          # Enums (Provider, Model, MessageRole)
├── Program.cs                # Entry point and DI configuration
└── appsettings.json          # Local configuration (not committed)
```

---


## Running the Tests

The test project uses **xUnit v3** and **Moq**. To run all tests:


dotnet test

Test coverage includes:
- `ChatService` — conversation history, system prompt, clear behaviour
- `Phi3Formatter` — token injection and prompt ordering
- `ShimmyProvider` — HTTP success/failure and formatter collaboration

---

## Roadmap

- [ ] OpenAI provider support
- [ ] Speech-to-text input (Vosk)
- [ ] Persistent conversation history
- [ ] Streaming responses
- [ ] Logging

---

## License

MIT License. See [LICENSE](LICENSE) for details.
