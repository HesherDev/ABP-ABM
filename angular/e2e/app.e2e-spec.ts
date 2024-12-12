import { ABMTemplatePage } from './app.po';

describe('ABM App', function() {
  let page: ABMTemplatePage;

  beforeEach(() => {
    page = new ABMTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
