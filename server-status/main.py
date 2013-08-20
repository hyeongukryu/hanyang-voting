# -*- coding: utf-8 -*-

import os, datetime, base64
import webapp2, jinja2

# Jinja2 템플릿
template_dir = os.path.join(os.path.dirname(__file__), 'template')

jinja2_env = jinja2.Environment(loader = jinja2.FileSystemLoader(template_dir), autoescape = True)

def render_template(template_name):
    return jinja2_env.get_template(template_name + '.html').render(nav=template_name)

class MainHandler(webapp2.RequestHandler):
    def get(self):
        self.response.write(render_template('main'))

app = webapp2.WSGIApplication([
    ('/', MainHandler)
], debug=True)
