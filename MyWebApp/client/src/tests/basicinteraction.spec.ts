import { test, expect } from '@playwright/test';

test('Make a sucessful purchase', async ({ page }) => {
  await page.goto('http://localhost:3000/');
  await page.getByRole('textbox', { name: 'Username' }).fill('Bach');
  await page.getByRole('spinbutton', { name: 'Quantity' }).fill('1');
  await page.getByRole('button', { name: 'Submit' }).click();
  await expect(page.getByText('Successful purchase')).toBeVisible();
});

test('Make a second purchase inmediatly', async ({ page }) => {
  await page.goto('http://localhost:3000/');
  await page.getByRole('textbox', { name: 'Username' }).fill('Bach');
  await page.getByRole('spinbutton', { name: 'Quantity' }).fill('1');
  await page.getByRole('button', { name: 'Submit' }).click();
  await expect(page.getByText('You are allowed to but 1 per minute. Please wait.')).toBeVisible();
});

test('User another user and purchase inmediatly', async ({ page }) => {
  await page.goto('http://localhost:3000/');
  await page.getByRole('textbox', { name: 'Username' }).fill('Pachelbel');
  await page.getByRole('spinbutton', { name: 'Quantity' }).fill('1');
  await page.getByRole('button', { name: 'Submit' }).click();
  await expect(page.getByText('Successful purchase')).toBeVisible();
});
// You are allowed to but 1 per minute. Please wait.
test('Unknow user', async ({ page }) => {
  await page.goto('http://localhost:3000/');
  await page.getByRole('textbox', { name: 'Username' }).fill('xxx');
  await page.getByRole('spinbutton', { name: 'Quantity' }).fill('1');
  await page.getByRole('button', { name: 'Submit' }).click();
  await expect(page.getByText('The use xxx is unknow.')).toBeVisible();
});

test('Quantity limit exceeded', async ({ page }) => {
  await page.goto('http://localhost:3000/');
  await page.getByRole('textbox', { name: 'Username' }).fill('Mozart');
  await page.getByRole('spinbutton', { name: 'Quantity' }).fill('2');
  await page.getByRole('button', { name: 'Submit' }).click();
  await expect(page.getByText('Quantity limit of 1 was exceeded, your current quantity is 2.')).toBeVisible();
});