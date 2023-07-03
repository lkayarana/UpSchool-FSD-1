# Password Generator with Memento Design Pattern

This project implements a password generator utility that utilizes the Memento design pattern to enable undo and redo functionality. The password generator allows you to generate secure passwords with various customizable options and provides the ability to undo and redo password changes.

https://github.com/lkayarana/UpSchool-FullStack-Development-Bootcamp/assets/102520514/d29e8aab-12e4-42b0-83f8-83308029a89d


## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Usage](#usage)
- [Technologies](#technologies)
- [Installation](#installation)

## Introduction

In today's digital world, having secure passwords is crucial to protect sensitive information. The Password Generator with Memento Design Pattern project aims to provide a solution by generating strong and customizable passwords. Additionally, it incorporates the Memento design pattern, allowing users to undo and redo password changes, providing flexibility and convenience.

## Features

- Generate secure passwords with customizable options:
  - Include numbers
  - Include lowercase characters
  - Include uppercase characters
  - Include special characters
- Undo and redo functionality for password changes
- Simple and intuitive interface

## Usage

To use the Password Generator utility with undo and redo functionality, follow these steps:

1. Clone the project repository.
2. Build and run the project in your preferred development environment.
3. Locate the PasswordGeneratorPage.razor file in the project and open it.
4. In the user interface, specify the password length and select the desired password options (include numbers, lowercase characters, uppercase characters, and special characters).
5. Click the "Generate Password" button to generate a new password.
6. To undo a password change, click the "Undo" button. This will revert the password to the previous state.
7. To redo a password change, click the "Redo" button. This will reapply a previously undone password change.
8. Repeat steps 4-7 to generate, undo, or redo passwords as needed.

Note: The Password Generator utility maintains a history of password changes, allowing multiple undo and redo operations.

## Technologies

The project is built using the following technologies:

- C# programming language
- .NET Core framework
- Razor Pages for the user interface

## Installation

To run the Password Generator with Memento Design Pattern project locally, follow these steps:

1. Clone the project repository to your local machine.
2. Open the project in your preferred development environment (e.g., Visual Studio).
3. Build the project to restore dependencies.
4. Run the project, and it should open in your default web browser.
5. Use the user interface to generate and manage passwords.
