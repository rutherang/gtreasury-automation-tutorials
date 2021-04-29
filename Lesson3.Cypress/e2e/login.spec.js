/// <reference types="cypress" />

describe("login module", () => {
  it("login as standard user", () => {
    cy.visit("");
    cy.getByTestId("username").type("standard_user");
    cy.getByTestId("password").type("secret_sauce");
    cy.getByTestId("login-button").click();
    cy.get("span.title").should("have.text", "Products");
  });

  it("login as lockout user", () => {
    cy.visit("");
    cy.getByTestId("username").type("locked_out_user");
    cy.getByTestId("password").type("secret_sauce");
    cy.getByTestId("login-button").click();

    cy.getByTestId("error").should("have.text", "Epic sadface: Sorry, this user has been locked out.");
  });
});
