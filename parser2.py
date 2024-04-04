import requests
from IPython.display import clear_output
from bs4 import BeautifulSoup as bs
import pandas as pd
##import pprint


spellsurl = pd.read_csv("./spellsurl.csv")
FILE_NAME = "spells.csv"

##print(spellsurl.count(0))
cnt = spellsurl['href'].count()
print(cnt)

def parse():
    result_list = [[['name'], ['lvl'], ['school'], ['source'], ['time'], ['distance'], ['components'], ['duration'], ['classes'], ['archtype'], ['description']]]
    d = 0
    for link in spellsurl['href']:
        clear_output(wait=True)
        print(link)
        d+=1
        print(d)        
        r = requests.get(link)
        soup = bs(r.text, "html.parser")
        spell_info = soup.find_all('div', class_='card-wrapper')
        tag = spell_info[1]

        name = tag.span.contents[0]

        spell_info = soup.find_all('ul', class_='params')
        tag =spell_info[0]
        i = 0
        lvl = tag.contents[i].text.split(',')[0]
        school = tag.contents[i].text.split(',')[1][1:] 
        i+=1
        time = tag.contents[i].text.split(':')[1][1:]
        i+=1
        distance = tag.contents[i].text.split(':')[1][1:]
        i+=1
        components = tag.contents[i].text.split(':')[1][1:]
        i+=1
        duration = tag.contents[i].text.split(':')[1][1:]
        i+=1
        classes = 'none'
        if (tag.contents[i].text.split(':')[0]=='Классы'):
            classes = tag.contents[i].text.split(':')[1][1:]
            i+=1
        archtype = 'none'
        if (tag.contents[i].text.split(':')[0]=='Архетипы'):
            archtype = tag.contents[6].text.split(':')[1][1:]
            i+=1
        source = tag.contents[i].text.split(':')[1][1:]   
        i+=1
        description = tag.contents[i].text
        result_list.append([[name], [lvl], [school], [source], [time], [distance], [components], [duration], [classes], [archtype], [description]])
    ##print (result_list)
    return result_list    
    
    ##print(tag.contents[i].text)
#print (result_list) 

df = pd.DataFrame(data=parse())
df.to_csv(FILE_NAME)

##spellsurl.tail(10)
