using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
 * This class is responsible for pausing and opening the book, as well as
 * filling the book's content. This class must be putted on the pause button and
 * the following parameters should be set:
 *  - itemsBook - the UI of the book
 *  - spriteHolder - the spriteHolder in the scene
 *  - emptySprite - a picture with a transperent canvas that will show whenever a page is empty
 */
public class ItemBookScript : MonoBehaviour
{
    public GameObject itemsBook;
    public SpriteHolder spriteHolder;
    public Sprite emptySprite;

    private List<string> curriculum = new List<string>();
    private List<string> names = new List<string>();

    private Text page1;
    private Text page2;

    private int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        //finds the texts that are children to the ui element
        page1 = itemsBook.transform.Find("Page1").GetComponent<Text>();
        page2 = itemsBook.transform.Find("Page2").GetComponent<Text>();
        
        fillCurriculum();
        
        //Sets first two pages 
        //sets the text
        page1.text = curriculum[currentPage];
        //sets the image by getting the name from the other list witch have the same index
        page1.transform.Find("Image").GetComponent<Image>().sprite = spriteHolder.getSprite(names[currentPage]);
        page2.text = curriculum[currentPage+1];
        page2.transform.Find("Image").GetComponent<Image>().sprite = spriteHolder.getSprite(names[currentPage+1]);
    }

    private void Update()
    {
        //checks if the P key is pressed, then openes the book
        if (Input.GetKeyDown(KeyCode.P) && !PlayerInput.isInventoryActive)
        {
            OpenBook();
        }
    }

    //This function is assigned to the NextPageButton
    public void nextPage()
    {
        currentPage += 2;
        //if the newly incremented value is bigger than the count that means that we are at the last page
        if(currentPage > curriculum.Count)
        {
            currentPage -= 2;
            return;
        }
        //if its equal to the count that means that we have only one page left and we fill the second with 
        //empty values
        if (currentPage == curriculum.Count - 1)
        {
            page1.text = curriculum[currentPage];
            page1.transform.Find("Image").GetComponent<Image>().sprite = spriteHolder.getSprite(names[currentPage]);
            page2.text = "";
            page2.transform.Find("Image").GetComponent<Image>().sprite = emptySprite;
        }
        //else we have two pages and we fill them
        else
        {
            page1.text = curriculum[currentPage];
            page1.transform.Find("Image").GetComponent<Image>().sprite = spriteHolder.getSprite(names[currentPage]);
            page2.text = curriculum[currentPage + 1];
            page2.transform.Find("Image").GetComponent<Image>().sprite = spriteHolder.getSprite(names[currentPage + 1]);
        }
    }

    

    public void previousPage()
    {
        currentPage -= 2;
        if (currentPage < 0)
        {
            currentPage = 0;
            return;
        }
        if (currentPage == curriculum.Count - 1)
        {
            page1.text = curriculum[currentPage];
            page1.transform.Find("Image").GetComponent<Image>().sprite = spriteHolder.getSprite(names[currentPage]);
            page2.text = "";
            page2.transform.Find("Image").GetComponent<Image>().sprite = emptySprite;
        }
        else
        {
            page1.text = curriculum[currentPage];
            page1.transform.Find("Image").GetComponent<Image>().sprite = spriteHolder.getSprite(names[currentPage]);
            page2.text = curriculum[currentPage + 1];
            page2.transform.Find("Image").GetComponent<Image>().sprite = spriteHolder.getSprite(names[currentPage + 1]);
        }
    }

    public void OpenBook()
    {
        Time.timeScale = 0;
        itemsBook.SetActive(true);
    }

    public void CloseBook()
    {
        Time.timeScale = 1;
        itemsBook.SetActive(false);
    }


    public void fillCurriculum()
    {
        //this adds a page
        curriculum.Add(@"The Revolver is a weapon that shoots rather slowly.
Damage: 10 health
        ");
        //this adds a name to the item that is described
        names.Add("Revolver");
        curriculum.Add(@"The Sniper is a long range weapon. You can use it if you want to shoot the enemies quickly
Damage: 50 health
        ");
        names.Add("Sniper");
        curriculum.Add(@"The Uzi is a short range weapon. It shoots fast and deals lot of damage
Damage: 20 health
        ");
        names.Add("Uzi");
        curriculum.Add(@"This is true Mother Russia weapon. Treat it carefully!
Damage: 30 health
        ");
        names.Add("AK_47");
        curriculum.Add(@"This is American power. If Rambo used it, you can too.
Damage: 50 health
        ");
        names.Add("М_60");
        curriculum.Add(@"The backpack is a fasionable cloath that will give you some protection.
Protection: 5%
        ");
        names.Add("Backpack");
        curriculum.Add(@"The gas mask will be there for you if someone farts. Also will protect you from bullets
Protection: 5%
        ");
        names.Add("GasMask");
        curriculum.Add(@"The helmet is the perfect military equipment. It also looks like 80's haircut
Protection: 10%
        ");
        names.Add("Helmet");
        curriculum.Add(@"This vest will protect you from bullets. It's in the name duhh...
Protection: 10%
        ");
        names.Add("BulletproofVest");
        curriculum.Add(@"This vest is from kevlar. It's tough!
Protection: 20%
        ");
        names.Add("KevlarVest");

    }

}
