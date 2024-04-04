import requests
from bs4 import BeautifulSoup as bs
import pandas as pd

FILE_NAME = "spellsurl.csv"
URL_TEMPLATE = "https://dnd.su/spells/"

##r = requests.get(URL_TEMPLATE)

def parse(url = URL_TEMPLATE):
    result_list = {'href': []}
    r = requests.get(url)
    soup = bs(r.text, "html.parser")
    spells_url = soup.find_all('div', class_='col list-item__spell for_filter')
##pprint(spells_name[0].a['href'])
    for url in spells_url:
        result_list['href'].append('https://dnd.su'+url.a['href'])
    ##print (result_list)
    return result_list
    
df = pd.DataFrame(data=parse())
df.to_csv(FILE_NAME)
