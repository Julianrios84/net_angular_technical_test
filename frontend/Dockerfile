FROM node:16.14 as node

ARG ENVIRONMENT

WORKDIR /app
COPY ./ /app/

# Install Packaged and Build App
RUN npm cache clean --force
RUN npm install
RUN npm run build:${ENVIRONMENT}
RUN mv /app/dist/${ENVIRONMENT}/* /app/dist/

COPY ./proxy.conf.json /app/dist/

# Serve app, based on Nginx, to have only the compiled app ready for production with Nginx
FROM nginx:1.13.8-alpine

COPY --from=node /app/dist/ /usr/share/nginx/html
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
