# Shopping Cart System Quiz 2 and 3

## Project Description
- This project is an enhanced console-based Shopping Cart System developed using C#. It allows users to browse products, manage a shopping cart, and complete checkout with payment validation. Building upon the original structure, this version introduces advanced functionality including real-time inventory management, automated receipt generation, and persistent order history tracking.

## Features
# Cart Management
- View cart items
- Remove item from cart
- Update item quantity
- Clear cart

# Product Features
- View all products
- Search product by name
- Filter products by category
  
# Checkout & Receipt System
- Calculates total price
- Applies discount (if applicable)
- Validates payment input
- Generates receipt number
- Displays date and time
  
# Inventory & History
- Updates stock after purchase
- Low Stock Alert
- Order History
  
# Input Validation
- Numeric Validation: Uses TryParse logic to prevent crashes from invalid text entry.
- Input Loops: Y/N prompts and menu choices loop until a valid input is received.
  
## Files Included
- Program.cs
- README.md

## Sample Output
<img width="565" height="327" alt="image" src="https://github.com/user-attachments/assets/0fdb273c-fbe0-43ea-933e-a9c208718645" />

##AI Usage in this Project
- During the development of this Shopping Cart System (Part 2), I used AI (Gemini and ChatGPT) as coding assistants to improve and debug my program.

# Prompts I asked AI: 
  - Create a C# Shopping Cart System using classes
  - How to properly validate user input using TryParse
  - How to fix issues with stock not updating correctly
  - How to implement receipt generation with date/time and receipt number
  - How to validate Y/N inputs properly until correct response is given
  - Implement order history using arrays
    
# Parts where I used AI:
  - Debugging: Troubleshooting stock deduction and ensuring correct inventory updates.
  - Input Validation: Improving TryParse logic to prevent crashes (invalid numbers, empty cart cases).
  - Logic Structure: Organizing the cart system to avoid duplicate errors and enhancing the checkout flow.
  - Feature Implementation: Guidance on implementing receipt generation (date/time/unique ID) and order history using arrays.

 # Changes I made after using AI:
  - Simplified Data Structures: I adapted the suggested logic to work with arrays to keep the project consistent with the foundation of the previous lessons.
  - Custom Safety Checks: Added logic for "empty cart" scenarios and "invalid index" selections.
  - Enhanced Validation: Integrated Y/N loops for critical menu actions to ensure valid user intent.
  - Integration: Every block of code was reviewed, typed, and tested to ensure full understanding of the logic before final submission.

Overall, AI was used only as a guide. I made sure to understand and apply the logic myself in completing the program.

