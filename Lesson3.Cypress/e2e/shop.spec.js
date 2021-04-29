/// <reference types="cypress" />

describe.only("shopping module", () => {
  beforeEach(() => {
    cy.visit("");
    cy.getByTestId("username").type("standard_user");
    cy.getByTestId("password").type("secret_sauce");
    cy.getByTestId("login-button").click();
  });

  it("can add item to cart", () => {
    // cy.visit("");
    // cy.getByTestId("username").type("standard_user");
    // cy.getByTestId("password").type("secret_sauce");
    // cy.getByTestId("login-button").click();

    cy.getByTestId("add-to-cart-sauce-labs-backpack").click();
    cy.getByTestId("add-to-cart-sauce-labs-fleece-jacket").click();
    cy.get(".shopping_cart_badge").should("have.text", "2");
  });

  it("can remove item from cart", () => {
    cy.getByTestId("add-to-cart-sauce-labs-backpack").click();
    cy.getByTestId("add-to-cart-sauce-labs-fleece-jacket").click();
    cy.wait(1000);
    cy.getByTestId("remove-sauce-labs-fleece-jacket").click();
    cy.get(".shopping_cart_badge").should("have.text", "1");
  });
});
